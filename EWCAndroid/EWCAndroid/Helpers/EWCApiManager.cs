using System;
using System.Threading.Tasks;
using ModernHttpClient;
using System.Net.Http;
using EWCAndroid.EWCAPI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using DataModel.Models.Entity;
using DataModel.Models.Response;
//using EWCAndroid.Helpers;
using DataModel.Models.Request;
using System.Net;
//using Foundation;
using Android.Util;
using EWCAndroid.Helpers;

namespace EWCAndroid
{
	public static class EWCApiManager
	{

		public static AuthenticationResponse MyTestToken = new AuthenticationResponse ();

		public static bool IsOnline {
			get { 
				return true;
//				return (Reachability.ReachabilityForInternet ().CurrentStatus != ReachabilityStatus.NotReachable);
			}
		}

		private static void InitClient(HttpClient client)
		{

			client.BaseAddress = new Uri(LocalGlobals.EWCAPIRoot);

			client.DefaultRequestHeaders.Accept.Clear();
		}


		public async static Task RefreshEWCProfile(string email, string token)
		{
			var response = await EWCApiManager.GetGuestProfile (email, token);

			if (response.Success  && response.Payload != null)
			{
				//AppDelegate.EWCUser.EWCUserProfile = response.Payload;
			}
		}

		public async static Task<EWCServiceResponse<GuestProfile>> GetGuestProfile(string email, string token)
		{
			var response = new EWCServiceResponse<GuestProfile>();
			response.Success = false;

			try
			{
				if (IsOnline) {

					using (var httpClient = new HttpClient (new NativeMessageHandler ())) {

						InitClient (httpClient);

						httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
						httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

						//Changed to GetAsync from GetStringAync for 2.5.3 so we can check status codes.
						var result = await httpClient.GetAsync (string.Format("{0}?email={1}", LocalGlobals.EWCAPIGetGuestProfile, email));

						if (result.IsSuccessStatusCode)
						{
							var data = await result.Content.ReadAsStringAsync();
							response = JsonConvert.DeserializeObject<EWCServiceResponse<GuestProfile>> (data);
						}
					}

				}
			}catch(Exception ex)
			{
				
				Log.Error ("GetGuestProfile",ex.Message.ToString());
			}

			return response;
		}

		public async static Task<EWCServiceResponse<GuestPoints>> GetGuestPoints(string globalId, string token)
		{
			var response = new EWCServiceResponse<GuestPoints>();
			response.Success = false;

			try
			{
				if (IsOnline) {

					using (var httpClient = new HttpClient (new NativeMessageHandler ())) {

						InitClient (httpClient);

						httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
						httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

						//Changed to GetAsync from GetStringAync for 2.5.3 so we can check status codes.
						var result = await httpClient.GetAsync (string.Format("{0}?globalId={1}", LocalGlobals.EWCAPIGetPoints, globalId));

						if (result.IsSuccessStatusCode)
						{
							var data = await result.Content.ReadAsStringAsync();
							response = JsonConvert.DeserializeObject<EWCServiceResponse<GuestPoints>> (data);
						}
					}

				}
			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("GetGuestPoints",ex.Message.ToString());
			}

			return response;
		}

		public async static Task<EWCServiceResponse<Package[]>> GetGuestPackages(string globalId, string token)
		{
			var response = new EWCServiceResponse<Package[]>();
			response.Success = false;

			try
			{
				if (IsOnline) {

					using (var httpClient = new HttpClient (new NativeMessageHandler ())) {

						InitClient (httpClient);

						httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
						httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

						//Changed to GetAsync from GetStringAync for 2.5.3 so we can check status codes.
						var result = await httpClient.GetAsync (string.Format("{0}?globalId={1}", LocalGlobals.EWCAPIGetPackages, globalId));

						if (result.IsSuccessStatusCode)
						{
							var data = await result.Content.ReadAsStringAsync();
							response = JsonConvert.DeserializeObject<EWCServiceResponse<Package[]>> (data);
						}
					}

				}
			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("GetGuestPackages",ex.Message.ToString());
			}

			return response;
		}

		public async static Task<EWCServiceResponse<NewGuestRequestResponse[]>> CreateGuest(NewGuestRequest request)
		{
			var response = new EWCServiceResponse<NewGuestRequestResponse[]>();

			try
			{
				if (IsOnline) {

					using (var httpClient = new HttpClient (new NativeMessageHandler ())) {

						InitClient (httpClient);

						httpClient.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue("application/json"));

						var result = await httpClient.PostAsync (LocalGlobals.EWCAPIPostCreateGuest, new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json"));

						if (result.IsSuccessStatusCode)
						{
							var data = await result.Content.ReadAsStringAsync();
							response = JsonConvert.DeserializeObject<EWCServiceResponse<NewGuestRequestResponse[]>> (data);
						}
					}
				}

			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("CreateGuest",ex.Message.ToString());
			}

			return response;
		}

		public async static Task<bool> UpdateGuestProfile(UpdateGuestProfileRequest request, string token)
		{
			bool success = false;

			try
			{
				if (IsOnline) {

					using (var httpClient = new HttpClient (new NativeMessageHandler ())) {

						InitClient (httpClient);

						httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

						httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

						var result = await httpClient.PutAsync (LocalGlobals.EWCAPIPutUpdateGuest, new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json"));

						if (result.IsSuccessStatusCode)
						{
							success = true;
						}
					}
				}

			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("UpdateGuestProfile",ex.Message.ToString());
			}

			return success;
		}

		public async static Task<HttpStatusCode> UpdateGuestPassword(UpdatePasswordRequest request, string token)
		{
			//assume failure or not online
			HttpStatusCode status = HttpStatusCode.NotFound;

			try
			{
				if (IsOnline) {

					using (var httpClient = new HttpClient (new NativeMessageHandler ())) {

						InitClient (httpClient);

						httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

						httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

						var result = await httpClient.PutAsync (LocalGlobals.EWCAPIPutUpdatePassword, new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json"));
						status = result.StatusCode;
					}
				}

			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("UpdateGuestPassword",ex.Message.ToString());
			}

			return status;
		}

		public async static Task<bool> UpdateGuestEmail(UpdateEmailRequest request, string token)
		{
			bool success = false;

			try
			{
				if (IsOnline) {

					using (var httpClient = new HttpClient (new NativeMessageHandler ())) {

						InitClient (httpClient);

						httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

						httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

						var result = await httpClient.PutAsync (LocalGlobals.EWCAPIPutUpdateEmail, new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json"));

						if (result.IsSuccessStatusCode)
						{
							success = true;
						}
					}
				}

			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("UpdateGuestEmail",ex.Message.ToString());
			}

			return success;
		}

		public async static Task<EWCServiceResponse<string>> ForgotPassword(ForgotPasswordRequest request)
		{
			var response = new EWCServiceResponse<string>();

			try
			{
				if (IsOnline) {

					using (var httpClient = new HttpClient (new NativeMessageHandler ())) {

						InitClient (httpClient);

						httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

						var result = await httpClient.PostAsync (LocalGlobals.EWCAPIPostForgotPassword, new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json"));

						if (result.IsSuccessStatusCode)
						{
							var data = await result.Content.ReadAsStringAsync();
							response = JsonConvert.DeserializeObject<EWCServiceResponse<string>> (data.ToString ());

							if(response.Payload == null)
								response.Success = false;
						}
					}
				}

			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("ForgotPassword",ex.Message.ToString());
			}

			return response;
		}

		public async static Task<AuthenticationResponse> GetAuthenticationToken(string email, string password)
		{
			try {
				using (var client = new HttpClient (new NativeMessageHandler ())) {
					InitClient (client);

					var content = new FormUrlEncodedContent (new[] {
						new KeyValuePair<string, string> ("grant_type", "password"),
						new KeyValuePair<string, string> ("username", email),
						new KeyValuePair<string, string> ("password", password),
						new KeyValuePair<string, string> ("client_id", LocalGlobals.IOSClientID),
						new KeyValuePair<string, string> ("client_secret", LocalGlobals.IOSClientSecret)
					});

					var response = await client.PostAsync (LocalGlobals.EWCAPIPostGetToken, content);

					if (response.IsSuccessStatusCode) {
						var result = JObject.Parse (await response.Content.ReadAsStringAsync ());
						var accessToken = result ["access_token"].Value<string> ();
						var user = result ["userName"].Value<string> ();
						var userToken = new AuthenticationResponse ();
						userToken.BearerToken = accessToken;
						userToken.UserName = user;
						return userToken;
					}
				}

			} catch (Exception ex) {
				Log.Error ("GetAuthenticationToken",ex.Message.ToString());
				//Log.LogError (ex);
			}

			return null;
		}
		//No Longer used
		public async static Task<ServiceResponse<DataModel.Models.Entity.User>> AddOrUpdateAzureProfile(NewGuestRequest request,string localID, LoginRequest loginRequest, string socialId, string imageUrl)
		{
			var user = new DataModel.Models.Entity.User () {
				City = loginRequest.City,
				Country = loginRequest.Country,
				CurrentBadgeCount = 0,
				DeviceId = string.Empty,
				Devicetype = Util.GetDeviceInfo (),
				Email = request.Email,
				FirstName = request.FirstName,
				Gender = request.Gender,
				HomeWaxCenter = string.Empty,
				HomeWaxCenterGeo = string.Empty,
				LastName = request.LastName,
				LocationGeo = loginRequest.LocationGeo,
				LoginToken = string.Empty,
				PhoneNumber = request.PhoneNumber,
				SocialLogin = socialId,
				StateProv = loginRequest.State,
				LocalId = localID,
				UserImage =  imageUrl
			};

			return await AddOrUpdateAzureProfile (user);
		}

		public async static Task<ServiceResponse<DataModel.Models.Entity.User>> AddOrUpdateAzureProfile(GuestProfile request, LoginRequest loginRequest, string socialId, string imageUrl)//, NewGuestRequestResponse newGuest)
		{

			//NSObject ver = NSBundle.MainBundle.InfoDictionary ["CFBundleShortVersionString"];

			var user = new DataModel.Models.Entity.User () {
				City = loginRequest.City,
				Country = loginRequest.Country,
				CurrentBadgeCount = 0,
				DeviceId = string.Empty,
//				Devicetype = Util.GetDeviceInfo () + " " + ver.ToString(),
				Devicetype = Util.GetDeviceInfo (),// + " " + ver.ToString(),
				Email = request.Email,
				FirstName = request.Firstname,
				Gender = request.Gender,
				GlobalId = request.GlobalId.ToString (),
				HomeWaxCenter = string.Empty,
				HomeWaxCenterGeo = string.Empty,
				LastName = request.Lastname,
				LocalId = request.LocalId.ToString (),
				LocationGeo = loginRequest.LocationGeo,
				LoginToken = string.Empty,
				PhoneNumber = request.PhoneNumber,
				SocialLogin = socialId,
				StateProv = loginRequest.State,
				UserImage =  imageUrl
			};

			return await AddOrUpdateAzureProfile (user);
		}


		public async static Task<ServiceResponse<DataModel.Models.Entity.User>>  AddOrUpdateAzureProfile(GuestProfile request)//, NewGuestRequestResponse newGuest)
		{

			//Loggly.LogToLoggly (JsonConvert.SerializeObject (request),"AddOrUpdateAzure");
			//NSObject ver = NSBundle.MainBundle.InfoDictionary ["CFBundleShortVersionString"];

			var user = new DataModel.Models.Entity.User () {
				CurrentBadgeCount = 0,
				DeviceId = string.Empty,
//				Devicetype = Util.GetDeviceInfo () + " " + ver.ToString(),
				Devicetype = Util.GetDeviceInfo (),// + " " + ver.ToString(),
				Email = request.Email,
				FirstName = request.Firstname,
				Gender = request.Gender,
				GlobalId = request.GlobalId.ToString (),
				HomeWaxCenter = string.Empty,
				HomeWaxCenterGeo = string.Empty,
				LastName = request.Lastname,
				LocalId = request.LocalId.ToString (),
				LoginToken = string.Empty,
				PhoneNumber = request.PhoneNumber,
			};


			return await AddOrUpdateAzureProfile (user);
		}

		public async static Task<ServiceResponse<DataModel.Models.Entity.User>> AddOrUpdateAzureProfile(User request)
		{
			ServiceResponse<DataModel.Models.Entity.User> user = new ServiceResponse<User> ();

			try
			{
				if (IsOnline) {

					user = await NetworkManager.ServicePost<User> (LocalGlobals.AzurePostAddOrUpdateGuest, JsonConvert.SerializeObject (request));

				}

			}catch(Exception ex)
			{
				user.Success = false;
				//Log.LogError (ex);
				Log.Error ("AddOrUpdateAzureProfile",ex.Message.ToString());
			}

			return user;
		}

		public async static Task<ServiceResponse<string>> UpdateHomeUserCenter(HomeCenter request)
		{
			//I am returning strin but it can be ignored and just look at Success
			ServiceResponse<string> homeCenter = new ServiceResponse<string> ();

			try
			{
				if (IsOnline) {

					homeCenter = await NetworkManager.ServicePost<string> (LocalGlobals.AzurePostUpdateUserHomeCenter, JsonConvert.SerializeObject (request));

				}

			}catch(Exception ex)
			{
				homeCenter.Success = false;
				//Log.LogError (ex);
				Log.Error ("UpdateHomeUserCenter",ex.Message.ToString());
			}
			//will return true is update was successful. 
			return homeCenter;
		}


		public async static Task<ServiceResponse<DataModel.Models.Entity.User>> GetAzureProfile(string email)
		{
			ServiceResponse<DataModel.Models.Entity.User> user = new ServiceResponse<User> ();

			try
			{
				if (IsOnline) {

					user = await NetworkManager.ServiceGet<User> (LocalGlobals.AzureGetGuestProfile + email);
					if(user.Result == null)
						user.Success = false;

				}

			}catch(Exception ex)
			{
				user.Success = false;
				//Log.LogError (ex);
				Log.Error ("GetAzureProfile",ex.Message.ToString());
			}

			return user;
		}

	}
}

