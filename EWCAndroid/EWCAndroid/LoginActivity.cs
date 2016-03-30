
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

namespace EWCAndroid
{
	[Activity (Label = "Login Activity")]			
	public partial class LoginActivity : Activity
	{
//		readonly LoginRequest _loginRequest = new LoginRequest();
//		ServiceResponse<User> _userResponse  = new ServiceResponse<User>();

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



	
	}
}

