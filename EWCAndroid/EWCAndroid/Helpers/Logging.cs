using System;
using EWCAndroid.Helpers;
using Newtonsoft.Json;
//using HockeyApp;
//using DataModel.Models.Entity;
//using MonoTouch.Foundation;

namespace EWCAndroid
{
	public static class Log
	{
		const string STANDARD_MESSAGE = "No Error Message Available";

		public static async void LogError (Exception ex)
		{
			try
			{

				NSObject ver = NSBundle.MainBundle.InfoDictionary ["CFBundleShortVersionString"];

				var err = new ErrorLog ();

				err.DeclaringObject = ex.TargetSite.DeclaringType.ToString ();
				err.MethodName = ex.TargetSite.ToString ();
				err.StackTrace = ex.Message.ToString () + " " + ex.StackTrace.ToString ();
				err.UserId = (AzureAuthentication.DefaultService.User != null) ? AzureAuthentication.DefaultService.Client.CurrentUser.UserId.ToString () : "Unknown";
				err.Device = ver.ToString() + " " + Util.GetDeviceInfo ();

				await NetworkManager.ServicePostWithNoResponse(LocalGlobals.LoggingEndPoint+"error",  JsonConvert.SerializeObject(err));

			}

			catch(Exception e)
			{
				#if DEBUG
				Console.WriteLine((e != null) ? e.Message.ToString() : STANDARD_MESSAGE);
				#endif
			}
		}

		public static async void LogInfo (string info, string container = "", string method = "")
		{
			try {

				NSObject ver = NSBundle.MainBundle.InfoDictionary ["CFBundleShortVersionString"];

				var err = new ErrorLog ();

				err.DeclaringObject = container;
				err.MethodName = method;
				err.StackTrace = info;
				err.UserId = (AzureAuthentication.DefaultService.User != null) ? AzureAuthentication.DefaultService.Client.CurrentUser.UserId.ToString () : "Unknown";
				err.Device = ver.ToString() + " " + Util.GetDeviceInfo ();

				await NetworkManager.ServicePostWithNoResponse(LocalGlobals.LoggingEndPoint+"info/", JsonConvert.SerializeObject (err));

			} catch (Exception e) {
				#if DEBUG
				Console.WriteLine((e != null) ? e.Message.ToString() : STANDARD_MESSAGE);
				#endif
			}
		}
	}
}

