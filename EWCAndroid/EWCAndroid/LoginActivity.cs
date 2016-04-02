
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using Android.Util;
using MonoDroid;

//using DataModel.Models.Request;
//using DataModel.Models.Response;
//using DataModel.Models.Entity;
using System.Threading.Tasks;
using DataModel.Models.Request;

namespace EWCAndroid
{
	[Activity (Label = "Login Activity")]			
	public partial class LoginActivity : Activity
	{
		readonly LoginRequest _loginRequest = new LoginRequest();
//		ServiceResponse<User> _userResponse  = new ServiceResponse<User>();

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			// Set our view from the " Login  qScreen" layout resource
			SetContentView (Resource.Layout.LoginScreen);

			TextView callForgotPasswordScreen = FindViewById<TextView> (Resource.Id.lblForgotPassword);
			callForgotPasswordScreen.Click += (sender, e) =>
			{
				Login();
				var intentForgotPassword = new Intent(this, typeof(ForgotPasswordActivity));
				StartActivity(intentForgotPassword);
			};
		}

//		string GetSocialLoginId()
//		{
//			if (this.IsSocialMediaLogin)
//			{
//				if (_socialMediaView.LoginType == LoginType.Google)
//					return "google:" + _googleUser.ID;
//				else if (_socialMediaView.LoginType == LoginType.Facebook)
//					return "facebook:" + _facebookUser.ID;
//				else if (_socialMediaView.LoginType == LoginType.Twitter)
//					return "twitter:" +_twitterUser.Screen_Name;
//
//			}
//
//			return string.Empty;
//		}
//
//		string GetSocialProfileImage()
//		{
//			if (this.IsSocialMediaLogin)
//			{
//				if (_socialMediaView.LoginType == LoginType.Google)
//					return  _googleUser.ProfileImageUrl;
//				else if (_socialMediaView.LoginType == LoginType.Facebook)
//					return _facebookUser.ProfileImageUrl;
//				else if (_socialMediaView.LoginType == LoginType.Twitter)
//					return _twitterUser.ProfileImageUrl;
//
//			}
//
//			return string.Empty;
//		}

		private async Task Login()
		{
			string email = string.Empty;
			string password = string.Empty;

			email = Resource.Id.txtEmail.ToString();
			password = Resource.Id.txtPassword.ToString();

			//Util.ShowWaitDialog ("Logging In...");

//			if (IsSocialMediaLogin) {
//				if (string.IsNullOrEmpty (email))
//					email = GetSocialEmail ();
//			}

//			if (!Util.ValidateEmail (email))
//			{	Util.DismissDialog ();
//				if (IsSocialMediaLogin) {
//					ShowError ("Almost done—just enter your email address and you’re ready to go.");
//				} else {
//					ShowError ("You're almost there—just enter your email address to sign in.");
//				}
//				return;
//			}
//
//			if (!IsSocialMediaLogin) {
//				if (string.IsNullOrWhiteSpace (password)) {
//					Util.DismissDialog ();
//					ShowError ("You're almost there—just enter your password to sign in.");
//					return;
//				}
//			}

			var authResponse = await EWCApiManager.GetAuthenticationToken (email, password);
//
//			if (authResponse == null || string.IsNullOrEmpty (authResponse.BearerToken))
//			{	Util.DismissDialog ();
//				ShowError ("We don’t recognize your sign-in information. Please try again or sign up to create a new account.");
//				return;
//			}
//
			var profile = await EWCApiManager.GetGuestProfile (email, authResponse.BearerToken);
//
//			if (!profile.Success)
//			{
//				Util.DismissDialog ();
//				ShowError ("We couldn't locate your profile, please try again.");
//				return;
//			}
//
//			await Loggly.LogToLoggly (JsonConvert.SerializeObject (profile), "Login Trace");
//
//			var serviceResponse = await EWCApiManager.AddOrUpdateAzureProfile (profile.Payload,_loginRequest,GetSocialLoginId(),GetSocialProfileImage());
//			Util.DismissDialog ();
//			if (!serviceResponse.Success)
//			{
//				ShowError ("We couldn't locate your profile, please try again. (AMS).");
//				return;
//			}
//
//			Util.SetGlobalUserProfile (serviceResponse.Result, profile.Payload, authResponse.BearerToken, GetSocialProfileImage ());
//
			Helpers.Util.RegistorForPush ();
//
//
//			PerformSegue ("LogInComplete", this);

		}
	
	}
}

