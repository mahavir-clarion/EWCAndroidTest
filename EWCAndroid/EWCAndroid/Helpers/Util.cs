using System;
//using MonoTouch.UIKit;
//using MonoTouch.Foundation;
using System.Text.RegularExpressions;
//using MonoTouch.CoreImage;
using System.Drawing;
using System.Threading.Tasks;
using System.Net.Http;
using Android.Webkit;
using Android;
using DataModel.Models.Entity;
using EWCAndroid.EWCAPI.Models;
using Android.App;
using Newtonsoft.Json;


namespace EWCAndroid.Helpers
{
	public static class Util
	{
		public static string GetDeviceInfo()
		{
			return	Android.OS.Build.Model + ":" + Android.OS.Build.Manufacturer + ":" + Android.OS.Build.VERSION.Codename;

			//return UIDevice.CurrentDevice.Model + ":" + UIDevice.CurrentDevice.SystemName + ":" + UIDevice.CurrentDevice.SystemVersion;
		}             

		public static double ConvertDistanceToMiles(double distance)
		{
			if (distance > 0) {
				return Math.Round ((distance * 0.000621371), 1);
			}else{
				return 0;
			}
		}

		// Displays a UIAlertView and returns the index of the button pressed.
		public static Task<int> ShowAlert (string title, string message, params string [] buttons)
		{
			
			var tcs = new TaskCompletionSource<int> ();
//			//var alert = new UIAlertView {
//				Title = title,
//				Message = message
//			};
//			foreach (var button in buttons)
//		    alert.AddButton (button);
//			alert.Clicked += (s, e) => tcs.TrySetResult (e.ButtonIndex);
//			alert.Show ();
			return tcs.Task;
		}
			
	

		public  static void RegistorForPush(){
			

//			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
//
//				var settings = UIUserNotificationSettings.GetSettingsForTypes (
//					UIUserNotificationType.Alert
//					| UIUserNotificationType.Badge
//					| UIUserNotificationType.Sound,
//					new NSSet ());
//				UIApplication.SharedApplication.RegisterUserNotificationSettings (settings); 
//				UIApplication.SharedApplication.RegisterForRemoteNotifications ();
//				// Register for Notifications
//
//			} else {
//				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes
//				(
//					UIRemoteNotificationType.Alert |
//					UIRemoteNotificationType.Badge |
//					UIRemoteNotificationType.Sound);
//			}
		}

		public static WebViewClient Blur(this WebViewClient image, float blurRadius = 25f)
//		public static UIImage Blur(this UIImage image, float blurRadius = 25f)
		{
			if (image != null)
			{
				// Create a new blurred image.
//				var inputImage = new CIImage (image);
//				var blur = new CIGaussianBlur ();
//				blur.Image = inputImage;
//				blur.Radius = blurRadius;
//
//				var outputImage = blur.OutputImage;
//				var context = CIContext.FromOptions (new CIContextOptions { UseSoftwareRenderer = false });
//				var cgImage = context.CreateCGImage (outputImage, new RectangleF (new PointF (0, 0), image.Size));
//				var newImage = UIImage.FromImage (cgImage);
//
//				// Clean up
//				inputImage.Dispose ();
//				context.Dispose ();
//				blur.Dispose ();
//				outputImage.Dispose ();
//				cgImage.Dispose ();
//
//				return newImage;
			}
			return null;
		}

//		static public string GetGlobalValue(string key)
//		{
//			if (key == string.Empty)
//				return string.Empty;
//
//			using (var settingsDict = new NSDictionary (NSBundle.MainBundle.PathForResource ("Globals/globals.plist", null))) {
//				return settingsDict.ValueForKey ((NSString)key).ToString();
//			};
//		}
		public static void ShowWaitDialog(string message)
		{
			ProgressDialog.Show(null, null, message);
				
		}

		public static void DismissDialog()
		{
			
			//MTMBProgressHUD.Dismiss ();
		}

		static public bool ValidateEmail(string email)
		{
			return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
		}

		public static void SetGlobalUserProfile(User azureUser, GuestProfile ewcGuest, string token, string image){

			EWCGuest guest = new EWCGuest ();

			guest.AzureUserProfile = azureUser;
			guest.EWCUserProfile = ewcGuest;
			guest.EWCToken = token;

			AppDelegate.EWCUser = guest;

			Util.SaveFullUserProfile (AppDelegate.EWCUser);

			EWCAuthentication.GetInstance ().UserEmail = ewcGuest.Email;

			EWCAuthentication.GetInstance ().SaveStoredValue (ewcGuest.Email,EWCAuthentication.StorageValueOptions.UserToken, token);
			EWCAuthentication.GetInstance ().SaveStoredValue (ewcGuest.Email,EWCAuthentication.StorageValueOptions.GlobalId, ewcGuest.GlobalId.ToString ());
			EWCAuthentication.GetInstance ().SaveStoredValue (ewcGuest.Email,EWCAuthentication.StorageValueOptions.AzureUserId, azureUser.ID.ToString ());
			EWCAuthentication.GetInstance ().SaveStoredValue (ewcGuest.Email,EWCAuthentication.StorageValueOptions.UserImage, image);

		}
		public static void SaveFullUserProfile(EWCGuest guest)
		{
			if(guest == null || !guest.IsValid)
			{
				return;
			}

			EWCAuthentication.GetInstance ().SaveStoredValue ( guest.EWCUserProfile.Email,EWCAuthentication.StorageValueOptions.EWCUserCompleteProfile, JsonConvert.SerializeObject (guest));
			EWCAuthentication.GetInstance ().SaveStoredValue (guest.EWCUserProfile.Email,EWCAuthentication.StorageValueOptions.GlobalId, guest.EWCUserProfile.GlobalId.ToString ());
			EWCAuthentication.GetInstance ().SaveStoredValue (guest.EWCUserProfile.Email,EWCAuthentication.StorageValueOptions.AzureUserId, guest.AzureUserProfile.ID.ToString ());
		}
		static public bool CompareEmail(string e1, string e2)
		{
			if(string.IsNullOrEmpty(e1))
			{
				return false;
			}
			return e1.Equals (e2, StringComparison.CurrentCultureIgnoreCase);
		}

//		static public UIColor WaxCenterMainColor()
//		{
//			//Pink-ish color Pantone 192
//			//return UIColor.FromRGB (255, 14, 73);
//			return UIColor.FromRGB (225, 0, 74);
//			//return UIColor.FromRGB (236, 0, 68);
//		}
	}
}

