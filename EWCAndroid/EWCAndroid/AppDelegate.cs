using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Android.Runtime;
using Mono;
using NUnit.Common;
using MonoDroid;

using System.Windows;


namespace EWCAndroid
{

	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations

		public override UIWindow Window {
			get;
			set;
		}


		public static User EWCUser {
			get;
			set;
		}

		public static bool IsGuestUser {
			get;
			set;
		}


		public static Content AppContent {
			get { return Content.ContentInstance;}
		}

		static AppDelegate(){

			iRate.SharedInstance.DaysUntilPrompt = 3;
			iRate.SharedInstance.UsesUntilPrompt = 6;

			iRate.SharedInstance.MessageTitle = "Hello Gorgeous!";
			iRate.SharedInstance.Message = "Care to rate your experience? We'd love to hear from you!";
			iRate.SharedInstance.CancelButtonLabel = "No, Thanks";
			iRate.SharedInstance.RemindButtonLabel = "Remind Me Later";
			iRate.SharedInstance.RateButtonLabel = "Rate It Now";
		}


		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{

			//We MUST wrap our setup in this block to wire up
			// Mono's SIGSEGV and SIGBUS signals
			HockeyApp.Setup.EnableCustomCrashReporting (() => {

				//Get the shared instance
				var manager = BITHockeyManager.SharedHockeyManager;

				//Configure it to use our APP_ID
				manager.Configure (LocalGlobals.HockeyAppId);

				manager.CrashManager.CrashManagerStatus = BITCrashManagerStatus.AutoSend;

				//Start the manager
				manager.StartManager ();

				//Authenticate (there are other authentication options)
				manager.Authenticator.AuthenticateInstallation ();
			
				//Rethrow any unhandled .NET exceptions as native iOS 
				// exceptions so the stack traces appear nicely in HockeyApp
				AppDomain.CurrentDomain.UnhandledException += (sender, e) => 
					Setup.ThrowExceptionAsNative(e.ExceptionObject);

				TaskScheduler.UnobservedTaskException += (sender, e) => 
					Setup.ThrowExceptionAsNative(e.Exception);
			});

//			if (launchOptions != null && launchOptions.ContainsKey (UIApplication.LaunchOptionsLocationKey)) {
//				Console.WriteLine ("LOCATION EVENT");
//				return true;
//			}

			GetAppContent ();

			UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

			NSObject ver = NSBundle.MainBundle.InfoDictionary ["CFBundleShortVersionString"];
			var appVer = Helpers.Configuration.GetStringStoredValue ("Version").ToString ();

			if (string.IsNullOrEmpty(appVer))
			{	//First time install or reinstall of removed app. Force login again
				AzureAuthentication.DefaultService.RemoveLocalAccountStores ();
				Helpers.Configuration.SetBoolStoredValue ("SizzleWatched", false);
			}

			if (appVer != ver.ToString())
			{
				//First time install
				//or reinstall
				//or update
				UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
			}

			Helpers.Configuration.SetStringStoredValue ("Version",ver.ToString ());

			FBSettings.DefaultAppID = LocalGlobals.FbAppId;
			FBSettings.DefaultDisplayName = LocalGlobals.FbDisplayName;

			AppDelegate.IsGuestUser = Helpers.Configuration.GetBoolStoredValue ("IsGuestUser");

			if (!AppDelegate.IsGuestUser) {

				AzureAuthentication.DefaultService.SetAzureUserCredentials ();

				if (AzureAuthentication.DefaultService.User != null) {
					Helpers.Util.RegistorForPush ();

					if (string.IsNullOrEmpty (Helpers.Configuration.GetStringStoredValue ("HasValidTwitterEmail")) && AzureAuthentication.DefaultService.LoggedInProvider () == MobileServiceAuthenticationProvider.Twitter.ToString ()) {
						this.Window.RootViewController = Window.RootViewController.Storyboard.InstantiateViewController ("EmailCaptureViewController") as EmailCaptureViewController;
					} else {
						this.Window.RootViewController = Window.RootViewController.Storyboard.InstantiateViewController ("MainViewController") as MainViewController;			
					}
				}

			}else{
				this.Window.RootViewController = Window.RootViewController.Storyboard.InstantiateViewController ("MainViewController") as MainViewController;			
				AppDelegate.EWCUser = new User () { FirstName = "EWC", LastName = "Guest", ID = 99999 };
				Helpers.Util.RegistorForPush ();
			}

			if (launchOptions != null && launchOptions.ContainsKey (UIApplication.LaunchOptionsRemoteNotificationKey)) {
				NSDictionary pushMessage = launchOptions.ObjectForKey (new NSString (UIApplication.LaunchOptionsRemoteNotificationKey)) as NSDictionary;
				ProcessNotificationWhileAppOpen (pushMessage, true);
			}

			if (launchOptions != null && launchOptions.ContainsKey (UIApplication.LaunchOptionsLocalNotificationKey)) {
				var localNotification = launchOptions [UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
				ProcessLocalNotificationWhileAppOpen (localNotification, true);
			}

			Helpers.BeaconRegion entryRegion = new BeaconRegion(LocalGlobals.EWCUUID, LocalGlobals.WaxCenterEntry);
			ShareLocationManager.StartMonitoring (entryRegion.Region);

			//GeoFencing monitoring wired up for future use
			///Need only to fetch lng/lat of home center
//			var geoRegion = new CLCircularRegion(new CLLocationCoordinate2D(homeCenter.lat, homeCenter.lng),1.00, Globals.GeofenceEntry);
//			ShareLocationManager.StartMonitoring(geoRegion);

			//This line is critical
			//without it the ProcressRemoteNots method will not
			//perform the Rate Visit segue when being call from a swipe->launch the app
			//(note this line is not needed when the app is already open)
			//for some reason this ensures the window is open and visible and therefore
			//allows the rating segue to occur.  Without it, nothing happens.

			uint cacheSizeMemory = 4*1024*1024; // 4MB
			uint cacheSizeDisk = 32*1024*1024; // 32MB
			NSUrlCache.SharedCache.DiskCapacity = cacheSizeDisk;
			NSUrlCache.SharedCache.MemoryCapacity = cacheSizeMemory;

			this.Window.MakeKeyAndVisible ();	
			return true;
		}
			
		public override void ReceiveMemoryWarning (UIApplication application)
		{
			NSUrlCache.SharedCache.RemoveAllCachedResponses();
		}

		async void GetAppContent()
		{
			var cnt = await NetworkManager.ServiceGetRawString(LocalGlobals.AppContentEndPoint);
			if (!string.IsNullOrEmpty(cnt)) {
				AppDelegate.AppContent.ServerContent = JObject.Parse(cnt);
			}
		}

		private static CLLocationManager _locationManager;
		public static CLLocationManager ShareLocationManager
		{
			get
			{
				if(_locationManager == null)
				{
					_locationManager = new CLLocationManager ();
					_locationManager.Delegate = new Helpers.CoreLocationManager ();
					if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
						_locationManager.RequestAlwaysAuthorization ();
					}

				}
				return _locationManager;
			}
		}
		public override void DidEnterBackground (UIApplication application)
		{
			if (AppContent.ServerContent != null) {
				Helpers.Configuration.SetStringStoredValue ("AppContent", AppContent.ServerContent.ToString ());
			}
			NSUrlCache.SharedCache.RemoveAllCachedResponses();
		}

		// This method is called as part of the transiton from background to active state.
		public async override void WillEnterForeground (UIApplication application)
		{
			var content = Helpers.Configuration.GetStringStoredValue ("AppContent");
			if (string.IsNullOrEmpty(content))
			{
				var cnt = await NetworkManager.ServiceGetRawString(LocalGlobals.AppContentEndPoint);
				if (!string.IsNullOrEmpty(cnt)) {
					AppDelegate.AppContent.ServerContent = JObject.Parse(cnt);
				}

			}else{
				AppContent.ServerContent = JObject.Parse(content);
			}
		
			//set the azure client.user token and id every time the app opens
			if (!AppDelegate.IsGuestUser) {
				AzureAuthentication.DefaultService.SetAzureUserCredentials ();
			}
		}
			
		public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			// We need to handle URLs by passing them to FBSession in order for SSO authentication
			// to work.
			return FBSession.ActiveSession.HandleOpenURL(url);
		}

		public override void OnActivated (UIApplication application)
		{
			if (UIApplication.SharedApplication.ApplicationIconBadgeNumber > 0) {
				//BigTed.BTProgressHUD.ShowToast ("You have pending notifications in iOS Notification Center", false, 3000);
				BigTed.BTProgressHUD.ShowToast("Miss an EWC alert?  Check iOS notification center",BigTed.ProgressHUD.ToastPosition.Bottom, 4000);
				UIApplication.SharedApplication.CancelAllLocalNotifications ();
				UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
			}

			// We need to properly handle activation of the application with regards to SSO
			//  (e.g., returning from iOS 6.0 authorization dialog or from fast app switching).
			FBSession.ActiveSession.HandleDidBecomeActive();
		}

		public override void ReceivedRemoteNotification (UIApplication application, NSDictionary userInfo)
		{
			// Process a notification received while the app was already open
			ProcessNotificationWhileAppOpen (userInfo, false);
		}

		public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
		{
			//App is open when here
			ProcessLocalNotificationWhileAppOpen (notification, false);
		}

		public  override void FailedToRegisterForRemoteNotifications (UIApplication application, NSError error)
		{
			Log.LogInfo (error.ToString());
		}

		public async override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			//For now, this is the formating that must be done to make it work
			//MS (Todd) will update the pacakge to make this automatic in a future version
			var token = deviceToken.Description.Trim ('<', '>').Replace (" ", "");

			try
			{
				if (NetworkManager.IsOnline)
				{
					Push push = AzureAuthentication.DefaultService.Client.GetPush ();
					await push.RegisterNativeAsync(deviceToken);
				}

			}
			catch (Exception e) 
			{
				Log.LogError (e);
			}
		}

		void ProcessLocalNotificationWhileAppOpen(UILocalNotification notification, bool fromFinishedLaunching)
		{
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;

			//App is now open so process
			try
			{
				if (!fromFinishedLaunching && UIApplication.SharedApplication.ApplicationState == UIApplicationState.Active)
				{
					if (!string.IsNullOrEmpty(notification.SoundName) && !notification.SoundName.Contains("Default"))
					{
						//This assumes that in your json payload you sent the sound filename (like sound.caf)
						// and that you've included it in your project directory as a Content Build type.
						var soundObj = MonoTouch.AudioToolbox.SystemSound.FromFile(notification.SoundName);
						soundObj.PlaySystemSound();
					}
					new UIAlertView(notification.AlertAction, notification.AlertBody, null, "OK", null).Show();
				}

			}catch(Exception ex)
			{
				Log.LogError (ex);
			}
		}

		void ProcessNotificationWhileAppOpen(NSDictionary userInfo, bool fromFinishedLaunching)
		{
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;

			//App is now open so process
			try
			{
				if (null != userInfo) {

					//Check to see if the dictionary has the aps key.  This is a remote notif
					if (userInfo.ContainsKey (new NSString ("aps"))) {

						//Get the aps dictionary
						NSDictionary aps = userInfo.ObjectForKey (new NSString ("aps")) as NSDictionary;

						NSDictionary alert;
						string body = string.Empty;
						string sound = string.Empty;
						string title = string.Empty;
						int badge = 0 ;

						if (aps.ContainsKey (new NSString ("alert"))) {
							alert = (aps [new NSString ("alert")] as NSDictionary);
							body = alert.ObjectForKey (new NSString ("body")) as NSString;
							title = alert.ObjectForKey (new NSString ("action-loc-key")) as NSString;
						}

						//Extract the sound string
						if (aps.ContainsKey (new NSString ("sound")))
							sound = (aps [new NSString ("sound")] as NSString).ToString ();

//						//Extract the badge
//						if (aps.ContainsKey (new NSString ("badge"))) {
//							string badgeStr = (aps [new NSString ("badge")] as NSObject).ToString ();
//							int.TryParse (badgeStr, out badge);
//							UIApplication.SharedApplication.ApplicationIconBadgeNumber -= 1;
//						}

						//Get Custom keys
						if (userInfo.ContainsKey(new NSString("Url")))
						{
							var launchWithCustomKeyValue = (userInfo[new NSString("Url")] as NSString).ToString();

							if (fromFinishedLaunching) {
								InvokeOnMainThread (delegate {  
									//Create and pop modal rating controller
									UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard_iPhone", null);
									WebController wc = storyboard.InstantiateViewController ("WebController") as WebController;
									wc.ModalPresentationStyle = UIModalPresentationStyle.CurrentContext;
									wc.URL = launchWithCustomKeyValue.ToString();
									this.Window.RootViewController = Window.RootViewController.Storyboard.InstantiateViewController ("MainViewController") as MainViewController;
									this.Window.MakeKeyAndVisible ();
									Window.RootViewController.PresentViewController (wc, true, null);
								});
							} else{
								NSNotificationCenter.DefaultCenter.PostNotificationName ("AppDidReceiveNotification",this, new NSDictionary ("ActionType", "Url", "Payload",launchWithCustomKeyValue));
							}
							return;
						}

						if (userInfo.ContainsKey (new NSString ("RateVisit"))) {
						
							NSDictionary rating = userInfo.ObjectForKey (new NSString ("RateVisit")) as NSDictionary;
							LocalPOSTransaction pos = new Models.LocalPOSTransaction () {
								LocationId = rating.ObjectForKey (new NSString ("Location")) as NSString,
								TransactionId = rating.ObjectForKey (new NSString ("Transaction")) as NSString,
								Services = rating.ObjectForKey (new NSString ("TransactionServices")) as NSString,
							};

							var transDate = rating.ObjectForKey (new NSString ("TransactionDate")) as NSString;
							pos.TransactionDate = (!string.IsNullOrEmpty (transDate)) ? Convert.ToDateTime (transDate) : DateTime.Now;


							if (fromFinishedLaunching) {
								InvokeOnMainThread (delegate {  
									//Create and pop modal rating controller
									UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard_iPhone", null);
									RatingController rc = storyboard.InstantiateViewController ("RatingController") as RatingController;
									rc.ModalPresentationStyle = UIModalPresentationStyle.CurrentContext;
									rc.ServiceTransaction = pos;
									this.Window.RootViewController = Window.RootViewController.Storyboard.InstantiateViewController ("MainViewController") as MainViewController;
									this.Window.MakeKeyAndVisible ();
									Window.RootViewController.PresentViewController (rc, true, null);
								});
							} else{
								NSNotificationCenter.DefaultCenter.PostNotificationName ("AppDidReceiveNotification", this, new NSDictionary ("ActionType", "Rate", "Payload", pos));
							}

							return;
						}
					}
				}
			}catch(Exception ex)
			{
				Log.LogError (ex);
			}
		}
	}
}


