
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
using DataModel.Models.Entity;

namespace EWCAndroid
{
	[Activity (Label = "Login Activity")]			
	public partial class LoginActivity : Activity
	{
//		readonly LoginRequest _loginRequest = new LoginRequest();
		ServiceResponse<User> _userResponse  = new ServiceResponse<User>();

		bool _locationFound = false;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			// Set our view from the " Login  qScreen" layout resource
			SetContentView (Resource.Layout.LoginScreen);

			TextView callForgotPasswordScreen = FindViewById<TextView> (Resource.Id.lblForgotPassword);
			callForgotPasswordScreen.Click += (sender, e) =>
			{
				var intentForgotPassword = new Intent(this, typeof(ForgotPasswordActivity));
				StartActivity(intentForgotPassword);
			};
		}

		private async void LoginIn(MobileServiceAuthenticationProvider provider)
		{
			try
			{
				if (!NetworkManager.IsOnline)
				{
					RunOnUiThread(() => new UIAlertView ("Connectivity", LocalGlobals.WifiDisabledMessage, null, "OK", null).Show ());
					return;
				}

				SocialManager social = new SocialManager ();

				AzureAuthentication.DefaultService.User = await AzureAuthentication.DefaultService.Client.LoginAsync(this, provider);

				if(AzureAuthentication.DefaultService.User != null){


					_userResponse = await NetworkManager.ServicePost<User>(LocalGlobals.LoginEndPoint,  JsonConvert.SerializeObject(_loginRequest));

					if(_userResponse.Success)
					{
						AzureAuthentication.DefaultService.SaveAndRegisterAccount();

						AppDelegate.EWCUser = _userResponse.Result;

						if (provider == MobileServiceAuthenticationProvider.Twitter) {
							social.SaveTwitterToken (JObject.Parse (_userResponse.Result.LoginToken.ToString ()));
						}else if (provider == MobileServiceAuthenticationProvider.Facebook)
						{
							social.SaveFacebookToken(_userResponse.Result.LoginToken.ToString());
						}

						Helpers.Util.RegistorForPush ();

						if(provider == MobileServiceAuthenticationProvider.Twitter)
						{
							PerformSegue("CaptureTwiiterEmail",this);
						}else
						{
							if (string.IsNullOrEmpty(Helpers.Configuration.GetStringStoredValue("HomeCenter")))
							{
								PerformSegue("GetHomeCenter",this);
							}else{
								PerformSegue("MainWindow",this);
							}
						}

					}else{
						new UIAlertView ("Azure Login", _userResponse.ErrorMessage, null, "OK", null).Show ();

					}
				}
			}
			catch (InvalidOperationException ex)
			{
				Log.LogError (ex);
				AzureAuthentication.DefaultService.RemoveLocalAccountStores ();
				AppDelegate.EWCUser = null;
				AzureAuthentication.DefaultService.Client.Logout ();
				if (!ex.Message.Contains ("Authentication was cancelled by the user")) {
					new UIAlertView ("Social Login", ex.Message, null, "OK", null).Show ();
				}
			}
			catch (Exception ex)
			{
				Log.LogError (ex);
				AzureAuthentication.DefaultService.RemoveLocalAccountStores ();
				AppDelegate.EWCUser = null;
				AzureAuthentication.DefaultService.Client.Logout ();
			}
		}
	}
}

