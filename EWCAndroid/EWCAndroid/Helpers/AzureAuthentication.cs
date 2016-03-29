using System.Net;
using System.Text;
using Xamarin.Auth;
using System.Linq;
using System.Net.Http;
//using MonoTouch.UIKit;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
//using WindowsAzure.MobileServices;
//using Microsoft.WindowsAzure.MobileServices;
using System.Threading;
//using ModernHttpClient;

namespace EWCAndroid.Helpers
{
	public class AzureAuthentication : DelegatingHandler
	{
		enum AccountStorageName : byte
		{
			LoginProviderName = 0,
			EWCLoginToken = 1
		}

		enum AccountStorageKeys : byte
		{
			AuthProvider = 0,
			Token = 1
		}

		string applicationURL = LocalGlobals.AMSRootURL;
		string applicationKey = LocalGlobals.AMSAppId;

		public MobileServiceClient Client { get; set; }
		public MobileServiceUser User { get ; set; }

		AzureAuthentication()
		{
			CurrentPlatform.Init ();

			// Initialize the Mobile Service client with your URL and key

			HttpMessageHandler[] azureHandlers = new HttpMessageHandler[2];
			azureHandlers [0] = this;
			azureHandlers [1] =  new NativeMessageHandler ();

			Client = new MobileServiceClient (applicationURL, applicationKey, azureHandlers);

		}

		static AzureAuthentication instance = null;
		public static AzureAuthentication DefaultService {
			get {
				if (instance == null)
				{
					instance = new AzureAuthentication ();
				}
				return instance;
			}
		}

		public string LoggedInProvider ()
		{
			var accounts =  AccountStore.Create().FindAccountsForService (AccountStorageName.LoginProviderName.ToString()).ToArray();

			if (accounts.Any())
			{
				return accounts[0].Properties[AccountStorageKeys.AuthProvider.ToString().ToLower()];

			}else{
				return string.Empty;
			}
		}

		public void SetAzureUserCredentials()
		{
			try{

				KeyChain.KeyChainAccount key= new KeyChain.KeyChainAccount();
				var accounts = key.FindAccountsForService (AccountStorageName.EWCLoginToken.ToString()).ToArray();
	 			if (accounts.Any())
				{
					User = new MobileServiceUser (accounts[0].Username);
					User.MobileServiceAuthenticationToken = accounts[0].Properties[AccountStorageKeys.Token.ToString().ToLower()];
					Client.CurrentUser = User;
				}
			}catch(Exception ex)
			{
				User = null;
				Client.CurrentUser = null;
			}
		}

		public void RemoveLocalAccountStores()
		{
			try{
				KeyChain.KeyChainAccount key= new KeyChain.KeyChainAccount();
				var providers = key.FindAccountsForService (AccountStorageName.LoginProviderName.ToString()).ToArray();
				var accounts = key.FindAccountsForService (AccountStorageName.EWCLoginToken.ToString()).ToArray();

				//Loggin screen comeing up so clear any saved accounts used explicitly by EWC
				//some account data can be left alone so don't delete
				if (providers.Any())
				{
					providers.ToList ().ForEach (x => key.Delete (x, AccountStorageName.LoginProviderName.ToString()));
				}
				if (accounts.Any())
				{
					accounts.ToList ().ForEach (x => key.Delete (x, AccountStorageName.EWCLoginToken.ToString()));
				}

			}catch(Exception ex)
			{
				Log.LogError (ex);
			}
		}

		public void SaveAndRegisterAccount()
		{
			var provider = User.UserId.Substring (0, User.UserId.IndexOf (":", StringComparison.CurrentCulture));
			AccountManager.SaveAccount(User.UserId, AccountStorageName.EWCLoginToken.ToString(),new Dictionary<string,string>(){{AccountStorageKeys.Token.ToString().ToLower(), User.MobileServiceAuthenticationToken}});
			AccountManager.SaveAccount(User.UserId, AccountStorageName.LoginProviderName.ToString(),new Dictionary<string,string>(){{AccountStorageKeys.AuthProvider.ToString().ToLower(),provider.ToString()}});
		}

		protected override async Task<HttpResponseMessage> SendAsync (HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (this.Client == null)
			{
				throw new InvalidOperationException("Please set the 'Client' property in this handler before using it.");
			}

			// Cloning the request
			var clonedRequest = await CloneRequest(request);
			var response = await base.SendAsync(clonedRequest, cancellationToken);

			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				//We got a 401
	
				try
				{
					string provider = LoggedInProvider();
					User  = await this.Client.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController, provider);
				
					// we're now logged in again.
					//save the latest token locally
					AccountManager.SaveAccount (User.UserId, AccountStorageName.EWCLoginToken.ToString(),new Dictionary<string,string>(){{AccountStorageKeys.Token.ToString().ToLower(), User.MobileServiceAuthenticationToken}});

					// Clone the request
					clonedRequest = await CloneRequest(request);

					clonedRequest.Headers.Remove("X-ZUMO-AUTH");
					// Set the authentication header
					clonedRequest.Headers.Add("X-ZUMO-AUTH", User.MobileServiceAuthenticationToken);

					// Resend the request
					response = await base.SendAsync(clonedRequest, cancellationToken);
				}
				catch (InvalidOperationException)
				{
					// user cancelled auth,  return the original response
					return response;
				}

			}

			return response;
		}

		private async Task<HttpRequestMessage> CloneRequest(HttpRequestMessage request)
		{
			var result = new HttpRequestMessage(request.Method, request.RequestUri);
			foreach (var header in request.Headers)
			{
				result.Headers.Add(header.Key, header.Value);
			}

			if (request.Content != null)
			{
				var requestBody = await request.Content.ReadAsStringAsync();
				//Add this if because without out it await push.RegisterNativeAsync (token); in appdelegate:
				//1) could not use AWAIT statment and 
				//2) registration was not taking place at all.
				//one the actions of registration passed an empty content type and it was failing
				//Worked with Todd and Henrik from MS on this and fixed it
				if (request.Content.Headers.ContentType != null) {
					var mediaType = request.Content.Headers.ContentType.MediaType;
					result.Content = new StringContent (requestBody, Encoding.UTF8, mediaType);
				}
				foreach (var header in request.Content.Headers)
				{
					if (!header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
					{
						result.Content.Headers.Add(header.Key, header.Value);
					}
				}
			}

			return result;
		}
	}
}

