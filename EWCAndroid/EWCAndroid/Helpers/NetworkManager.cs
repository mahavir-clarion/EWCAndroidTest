using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using EWCAndroid.Helpers;
using DataModel.Models.Response;
using Newtonsoft.Json;
using System.Net.Http;
using ModernHttpClient;
//using Foundation;
using Android.Util;


namespace EWCAndroid
{
	public static class NetworkManager
	{
		public static Action<string> OnConnectionChanged;


		public static bool IsOnline {
			get { 
				return true;
				//return (Reachability.ReachabilityForInternet ().CurrentStatus != ReachabilityStatus.NotReachable);
			}
		}

		public static void DisplayConnectionWarnings()
		{
			//BigTed.BTProgressHUD.ShowToast ("No Internet Connection Available", false, 2000);
		}

		public async static Task<ServiceResponse<T>> ServiceGet<T>(string url)
		{
			try
			{
				if (IsOnline) {

					var result = await  AzureAuthentication.DefaultService.Client.InvokeApiAsync (url, System.Net.Http.HttpMethod.Get, null);

					var response = JsonConvert.DeserializeObject<ServiceResponse<T>> (result.ToString ());

					if (response.Success) {
						return response;
					}
				}
			}catch(Exception ex)
			{
				Log.Error ("ServiceGet",ex.Message.ToString());
				//Log.LogError (ex);
			}
			return new ServiceResponse<T> ();
		}

		public async static Task<string> ServiceGetRawString(string url)
		{
			try{
				if (IsOnline) {

					var result = await AzureAuthentication.DefaultService.Client.InvokeApiAsync (url, System.Net.Http.HttpMethod.Get, null);

					if (result != null && result.HasValues) {
						return result.ToString ();
					}
				}
			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("ServiceGetRawString",ex.Message.ToString());
			}
			return string.Empty;
		}

		public async static Task<ServiceResponse<T>> ServicePost<T>(string url, string content)
		{
			try{
				if (IsOnline) {

					var result = await AzureAuthentication.DefaultService.Client.InvokeApiAsync(url, content);

					var response = JsonConvert.DeserializeObject<ServiceResponse<T>>(result.ToString());

					if (response.Success) {
						return response;
					}
				}
			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("ServicePost",ex.Message.ToString());
			}
			return new ServiceResponse<T> ();
		}

		public static void ServicePostNoServiceResponse<T>(string url, string content)
		{
			try{
				if (IsOnline) {

					AzureAuthentication.DefaultService.Client.InvokeApiAsync(url, content);
				}
			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("ServicePostNoServiceResponse",ex.Message.ToString());
			}
		}

		public async static Task<ServiceResponse<T>> ServicePostUri<T>(string url)
		{
			try{
				if (IsOnline) {

					var result = await AzureAuthentication.DefaultService.Client.InvokeApiAsync(url);

					var response = JsonConvert.DeserializeObject<ServiceResponse<T>>(result.ToString());

					if (response.Success) {
						return response;
					}
				}
			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("ServicePostUri",ex.Message.ToString());
			}
			return new ServiceResponse<T> ();
		}

		public static void ServicePostWithNoResponse(string url, string content)
		{
			try{
				if (IsOnline) {

					AzureAuthentication.DefaultService.Client.InvokeApiAsync(url, content);
				}
			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("ServicePostWithNoResponse",ex.Message.ToString());
			}
		}

		public static async Task<bool> AzureServiceIsUp()
		{
			try{
				if (IsOnline) {
					using (var httpClient = new HttpClient (new NativeMessageHandler ())) {
						var task = await httpClient.GetAsync (LocalGlobals.AzureStatusURL);
						return (task.StatusCode == System.Net.HttpStatusCode.OK);
					}
				}
			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("AzureServiceIsUp",ex.Message.ToString());
			}
			//if the network connection is download let the app handle it
			//it doesn't mean azure is down
			return true;
		}
	}
}

