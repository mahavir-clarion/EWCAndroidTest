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
using Android.Content.PM;
namespace EWCAndroid
{
	
	[Activity (Label = "EWCAndroid", MainLauncher = true, Icon = "@mipmap/icon",ScreenOrientation=ScreenOrientation.Portrait)]
	public class MainActivity : Activity
	{
		//int count = 1;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.Main);

			ImageView callLoginScreen = FindViewById<ImageView> (Resource.Id.imageView2);
			callLoginScreen.Click += (sender, e) =>
			{
				Intent shareIntent = new Intent(Intent.ActionSend);
				shareIntent.SetType("text/plain");
				shareIntent.PutExtra(Android.Content.Intent.ExtraText,"TestText");
				shareIntent.PutExtra(Android.Content.Intent.ExtraSubject, "TestSubject");

				StartActivity (shareIntent);

				//				var intentLogin = new Intent(this, typeof(MainActivity));
				//				StartActivity(intentLogin);
			};


			ImageView callMenuList = FindViewById<ImageView> (Resource.Id.imgMenu);
			callMenuList.Click += (sender, e) =>
			{
				var menuList = new Intent(this, typeof(MenuListActivity));
				StartActivity(menuList);
			};
			#region Login Page

//			// Set our view from the " Defualt Screen" layout resource
//			SetContentView (Resource.Layout.FirstScreen);
//
//
//
//			// Call Login Screen
//			Button callLoginScreen = FindViewById<Button> (Resource.Id.btnLogIn);
//			callLoginScreen.Click += (sender, e) =>
//			{
//				var intentLogin = new Intent(this, typeof(LoginActivity));
//				StartActivity(intentLogin);
//			};
//
//			// Call Create Account Screen
//			Button callCreateAccountScreen = FindViewById<Button> (Resource.Id.btnCreateAccount);
//			callCreateAccountScreen.Click += (sender, e) => 
//			{
//				var intentCreateAccount = new Intent(this, typeof(CreateAccountActivity));
//				StartActivity(intentCreateAccount);
//			};

			#endregion
		}
	}
}


