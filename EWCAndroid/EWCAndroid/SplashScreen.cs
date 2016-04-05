
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Resources;
using System.Threading.Tasks;

namespace EWCAndroid
{

	//	[Activity (Label = "Splash Screen", MainLauncher=true, NoHistory=true, Theme="@styles/Theme.Splash")]			
	[Activity (Label = "Splash Screen", MainLauncher=true, NoHistory=true)]			
	public class SplashScreen : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.SplashScreen);
			// Create your application here


			Task startupWork = new Task(() => {
				//Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
				Task.Delay(150000);  // Simulate a bit of startup work.
				//Log.Debug(TAG, "Working in the background - important stuff.");
			});

			startupWork.ContinueWith(t => {
				//Log.Debug(TAG, "Work is finished - start Activity1.");
				StartActivity(new Intent(Application.Context, typeof(MainActivity)));
			}, TaskScheduler.FromCurrentSynchronizationContext());

			startupWork.Start();

			//StartActivity (typeof(MainActivity));
		}
	}
}

