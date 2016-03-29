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

namespace EWCAndroid
{
	[Activity (Label = "EWCAndroid", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		//int count = 1;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the " Defualt Screen" layout resource
			SetContentView (Resource.Layout.FirstScreen);

			// Call Login Screen
			Button callLoginScreen = FindViewById<Button> (Resource.Id.btnLogIn);
			callLoginScreen.Click += (sender, e) =>
			{
				var intentLogin = new Intent(this, typeof(LoginActivity));
				StartActivity(intentLogin);
			};

			// Call Create Account Screen
			Button callCreateAccountScreen = FindViewById<Button> (Resource.Id.btnCreateAccount);
			callCreateAccountScreen.Click += (sender, e) => 
			{
				var intentCreateAccount = new Intent(this, typeof(CreateAccountActivity));
				StartActivity(intentCreateAccount);
			};
		}
	}
}


