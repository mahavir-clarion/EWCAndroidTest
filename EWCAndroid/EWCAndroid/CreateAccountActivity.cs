
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
using EWCAndroid.Helpers;
using EWCAndroid.EWCAPI;
using System.Threading.Tasks;
using EWCAndroid.EWCAPI.Models;

using System.Net;
using DataModel.Models.Request;


namespace EWCAndroid
{
	[Activity (Label = "Create Account")]			
	public class CreateAccountActivity : Activity
	{
		private Java.Lang.Object _keyboardOpenObserver;
		private Java.Lang.Object _keyboardCloseObserver;

		private List<NewGuestRequestResponse> _existingAccounts;
		private NewGuestRequest _requestedAccount;

		readonly LoginRequest _loginRequest = new LoginRequest();

		public bool IsSocialMediaLogin { get; set; }
		//private SocialLoginView _socialMediaView;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.CreateAccountScreen);
			// Create your application here
			ShowError("This is for testing");

			Button btnCreateAccount = FindViewById<Button> (Resource.Id.btnCreateAccount);

			btnCreateAccount.Click += delegate {
				 CreateAndSignUp ();
			};


		}

		private async Task CreateAndSignUp(bool createOverride = false)
		{
			EditText txtFullName = FindViewById<EditText> (Resource.Id.txtFullName);
			EditText txtEmail = FindViewById<EditText> (Resource.Id.txtEmail);
			EditText txtPassword = FindViewById<EditText> (Resource.Id.txtPassword);
			EditText txtPhoneNumber = FindViewById<EditText> (Resource.Id.txtPhoneNumber);

			// NOT coming from selecting an existing user from the multiple existing user list
			var newGuest = new NewGuestRequest () 
			{
				
				Email = txtEmail.Text.ToString(),
//				BeenToEwcBefore = BeenToEWCBefore,
				BeenToEwcBefore = false,// Resource.Id.segBeenThere,
				BookingLocationId = 0,
				CreateOverride = (createOverride) ? 1 : 0,
				FirstName = GetFirstName(),
				LastName = GetLastName(),
				Gender = "M",//Resource.Id.segGender.ToString(),
				Password = txtPassword.Text.ToString(),
				PhoneNumber = txtPhoneNumber.Text.ToString()
			};

			await SignUp (newGuest);
		}

		private async Task SignUp(NewGuestRequest newGuest)
		{
			//Util.ShowWaitDialog ("Creating EWC Account...");
			var response = await EWCApiManager.CreateGuest (newGuest);


			//await Loggly.LogToLoggly (JsonConvert.SerializeObject (newGuest), "Profile Create");

			if (!response.Success) {
				Util.DismissDialog ();

				if (response.ErrorCode == HttpStatusCode.Conflict) { // 409
					ShowError ("Email already exists. Please login or use the Forgot Password to recover password.");
					return;
				}

				if (response.ErrorCode == HttpStatusCode.Ambiguous) { // 300
					// Multiple accounts exist
					_requestedAccount = newGuest;
					_existingAccounts = new List<NewGuestRequestResponse> (response.Payload);
					//PerformSegue ("segueToMultipleAccounts", this);
					return;
				}

				if (response.ErrorCode == HttpStatusCode.BadRequest) { // 400 - validation error
					ShowError (response.ErrorMessage);
					return;
				}

				if (response != null)
					//await Loggly.LogToLoggly (JsonConvert.SerializeObject (response), "Error");


				ShowError ("An unknown error occurred. Please try again.");
				return;
			}

			var auth = await EWCApiManager.GetAuthenticationToken (newGuest.Email, newGuest.Password);
			if (string.IsNullOrEmpty (auth.BearerToken))
			{
				Util.DismissDialog ();
				ShowError ("Cannot login at this time. Please try again later.");
				return;
			}

			var profile = await EWCApiManager.GetGuestProfile (newGuest.Email, auth.BearerToken);
			Util.DismissDialog ();
			if (!profile.Success)
			{
				ShowError ("Error retrieving profile. Please try again.");
				return;
			}

			var serviceResponse = await EWCApiManager.AddOrUpdateAzureProfile (profile.Payload, _loginRequest,GetSocialLoginId(),GetSocialProfileImage());
			if (!serviceResponse.Success)
			{
				Util.DismissDialog ();
				ShowError ("Unable to retrieve user profile (AMS).");
				return;
			}

			Util.SetGlobalUserProfile (serviceResponse.Result, profile.Payload, auth.BearerToken, GetSocialProfileImage ());

			Helpers.Util.RegistorForPush ();

			//PerformSegue ("segueToGetHomeCenter", this);
			//PerformSegue ("SignUpComplete", this);
		}

		private string GetFirstName()
		{
			var nameArray = SplitName ();
			if (nameArray.Length > 2)
				return string.Format ("{0} {1}", nameArray [0], nameArray [1]);
			else
				return nameArray [0];
		}

		private string GetLastName()
		{
			var nameArray = SplitName ();

			// only first name was entered
			if (nameArray.Length == 1)
				return string.Empty;

			// first and last name were entered
			if (nameArray.Length == 2)
				return nameArray [1];

			// more than 2 names entered, skip first 2 names, and use everyting else as last name
			var notFirstName = nameArray.Skip (2);
			return string.Join (" ", notFirstName);
		}

		private string[] SplitName()
		{
			EditText txtFullName = FindViewById<EditText> (Resource.Id.txtFullName);
			return txtFullName.Text.ToString().Split (new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries);
		}

		string GetSocialProfileImage()
		{
			if (this.IsSocialMediaLogin)
			{
//				if (_socialMediaView.LoginType == LoginType.Google)
//					return  GoogleUser.ProfileImageUrl;
//				else if (_socialMediaView.LoginType == LoginType.Facebook)
//					return FacebookUser.ProfileImageUrl;
//				else if (_socialMediaView.LoginType == LoginType.Twitter)
//					return TwitterUser.ProfileImageUrl;
			}

			return string.Empty;
		}


		string GetSocialLoginId()
		{
			if (this.IsSocialMediaLogin)
			{
//				if (_socialMediaView.LoginType == LoginType.Google)
//					return "google:" + GoogleUser.ID;
//				else if (_socialMediaView.LoginType == LoginType.Facebook)
//					return "facebook:" + FacebookUser.ID;
//				else if (_socialMediaView.LoginType == LoginType.Twitter)
//					return "twitter:" + TwitterUser.Screen_Name;

			}

			return string.Empty;
		}

		private void ShowError(string message)
		{
			//set alert for executing the task
			AlertDialog.Builder alert = new AlertDialog.Builder (this);

			alert.SetTitle("Hello Gorgeous!");
			alert.SetMessage (message);
			alert.SetPositiveButton ("OK", (senderAlert, args) => {
				//change value write your own set of instructions
				//you can also create an event for the same in xamarin
				//instead of writing things here
			} );

			//run the alert in UI thread to display in the screen
			RunOnUiThread (() => {
				alert.Show();
			} );


			//new UIAlertView ("Hello Gorgeous!", message, null, "OK", null).Show ();
			//Toast.MakeText ("Hello Gorgeous!", message, ToastLength.Short).Show ()	;
		}
	}
}

