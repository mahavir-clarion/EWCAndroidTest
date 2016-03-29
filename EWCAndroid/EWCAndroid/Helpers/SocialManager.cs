using System;
using Xamarin.Auth;

//using MonoTouch.FacebookConnect;
//using MonoTouch.UIKit;
using System.Collections.Generic;
//using Xamarin.Social.Services;
//using Xamarin.Social;
//using MonoTouch.Foundation;
//using MonoTouch.MessageUI;
using Newtonsoft.Json.Linq;
//using DataModel.Models.Entity;

namespace EWCAndroid.Helpers
{
	public class SocialManager
	{

		// https://developers.facebook.com/docs/reference/api/permissions/
		private string [] ExtendedfbPermissions = new [] { "public_profile","email","user_friends"};
		private const string TWITTER_CALLBACK = "https://waxcenter.azure-mobile.net/signin-twitter";
		private const string WEBSITE = "http://www.waxcenter.com";

		public SocialManager ()
		{
		}

		public void SaveFacebookToken(string azurefbToken)
		{
			var tokenCachingStrategy = new FBSessionTokenCachingStrategy ("AzurefbToken");
			var token = tokenCachingStrategy.FetchFBAccessTokenData ();
			FBAccessTokenData.CreateToken(azurefbToken,ExtendedfbPermissions,null,FBSessionLoginType.None,null);
			tokenCachingStrategy.CacheFBAccessTokenData (FBAccessTokenData.CreateToken (azurefbToken, ExtendedfbPermissions, null, FBSessionLoginType.SystemAccount, null));
		}

		public void LoginToFacebook(bool withUI = false)
		{
			var tokenCachingStrategy = new FBSessionTokenCachingStrategy ("AzurefbToken");
			FBSession session = new FBSession(null, ExtendedfbPermissions,null, tokenCachingStrategy);
			FBSession.ActiveSession = session; // This is where your session info will be store

			FBSession.OpenActiveSession(null,withUI,(currentSession,currentState, error)  => {
				//Do Nothing..user will be prompted to login
			});

		}

		public void SaveTwitterToken(JObject twitterTokens)
		{
			Dictionary<string,string> twitterDic = new Dictionary<string,string> ();

			foreach (var x in twitterTokens) 
			{
				twitterDic.Add (x.Key.ToString(), x.Value.ToString());
			}
			AccountManager.SaveAccount((string)twitterTokens["screen_name"].ToString(),"Twitter", twitterDic);
		}


		public void FacebookShareItem(UIViewController view, string picture, string link, string linkDesc, string linkTitle, string friend = ""){

			var friends = new NSString[1] { (NSString)friend };

			FBShareDialogParams linkParams = new FBShareDialogParams () {
				Picture =  new NSUrl(picture),
				LinkDescription = linkDesc,
				Link = new NSUrl ((string.IsNullOrEmpty(link) ? WEBSITE : link)),
				Name = linkTitle,
				Friends = friends
			};
			FacebookShare (view, linkParams);

		}

		void FacebookShare(UIViewController view, FBShareDialogParams shareParams)
		{
			if( AzureAuthentication.DefaultService.LoggedInProvider() == "Facebook")
			{
				LoginToFacebook (false);
			}

			bool nativeShareDialog = FBDialogs.CanPresentShareDialog (shareParams);

			if (nativeShareDialog) {
				FBDialogs.PresentShareDialog (shareParams ,null, (call, results, error) => {
					//Do nothing
				});
			} else {
				bool ios6ShareDialog = FBDialogs.CanPresentOSIntegratedShareDialog (FBSession.ActiveSession);
				if (ios6ShareDialog) {
					FBDialogs.PresentOSIntegratedShareDialogModally (view,shareParams.Name + "\n\n" + shareParams.Description , ImageManager.FromUrl(shareParams.Picture.AbsoluteString),shareParams.Link, (result, error) => {
						//Do Nothing
					});
				} else {
					var data = new NSMutableDictionary ();
					data.Add (new NSString ("name"), new NSString (shareParams.Name));
					data.Add (new NSString ("description"), new NSString (shareParams.Link.AbsoluteString));
					data.Add (new NSString ("link"), new NSString (shareParams.Link.AbsoluteString));
					data.Add (new NSString ("picture"), new NSString (shareParams.Picture.AbsoluteString));
					FBWebDialogs.PresentFeedDialogModally (null, data, (result, resultUrl, error) => {
						//Do Nothing
					});
				}
			}
		}


		public  void TwitterShare(UIViewController view, string message, string imageUrl, string link )
		{
			//TwitterService is looking or a Twitter OAuth Account stored locally
			//as part of the Xamarin Social componenet.
			//I save the twitter account/oauth data upon logging in with a twitter
			//account in the exact format it wants.
			var twitter = new TwitterService () {
				CallbackUrl = new Uri (TWITTER_CALLBACK),
				ConsumerKey = AppDelegate.AppContent.StringFromProperty (Globals.TwitterConsumerKey),
				ConsumerSecret = AppDelegate.AppContent.StringFromProperty (Globals.TwitterConsumerSecret),
			};

			Item item = new Item {
				Text = message,
				Links = new List<Uri> {
					new Uri (link),
				},
				//Images are causing a 403 error
				Images = new List<ImageData>()
				{
					new ImageData(ImageManager.FromUrl(imageUrl))
				}
			};

			UIViewController vc = twitter.GetShareUI (item, shareResult => {
				// result lets you know if the user shared the item or canceled
				if (shareResult.ToString().Contains("Cancelled")){
					item.Dispose();
				}
				view.DismissViewController (true, null);

			});
			view.PresentViewController (vc, true, null);
		}

		public void ShareEmail(UIViewController view, string subject, string message)
		{
			MFMailComposeViewController mailController = new MFMailComposeViewController();
			mailController.SetSubject (subject);
			mailController.SetMessageBody (message, true);

			mailController.Finished += ( object s, MFComposeResultEventArgs args) => {
				args.Controller.DismissViewController (true, null);
			};

			view.PresentViewController (mailController, true, null);
		}

		public void SendSMS (UIViewController view, string message, string imageURL)       
		{  //Init View Controller            

			var messageController = new MFMessageComposeViewController ();
			//Verify app can send text message             
			if(MFMessageComposeViewController.CanSendText)             
			{                 
				messageController.Body = message;  
				messageController.AddAttachment (ImageManager.FromUrl(imageURL).AsPNG ()  , "kUTTypePNG", "image.png"); 


				messageController.Finished += ( object s, MFMessageComposeResultEventArgs args) => args.Controller.DismissViewController (true, null);

				view.PresentViewController (messageController, true, null);
			}       
		}
	}

}

