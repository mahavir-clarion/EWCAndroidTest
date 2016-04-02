using System;
//using KeyChain;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using EWCAndroid.Helpers;
using DataModel.Models.Entity;
using System.Threading.Tasks;
using EWCAndroid.EWCAPI.Models;
using Android.Util;

namespace EWCAndroid
{
	public sealed class EWCAuthentication
	{
		private readonly static EWCAuthentication _instance =  new EWCAuthentication();
		const string APPNAME = "EWCGuestApp";

		public enum StorageValueOptions  
		{
			AuthEmailAddress,
			UserToken,
			GlobalId,
			AzureUserId,
			UserImage,
			EWCUserCompleteProfile
		};

		public string UserEmail {
			get
			{
				return Helpers.Configuration.GetStringStoredValue(StorageValueOptions.AuthEmailAddress.ToString ());
			}
			set
			{
				Helpers.Configuration.SetStringStoredValue(StorageValueOptions.AuthEmailAddress.ToString (), value);
			}
		}

		public bool IsGuestUser {
			get
			{
				return string.IsNullOrEmpty (Helpers.Configuration.GetStringStoredValue (StorageValueOptions.AuthEmailAddress.ToString ()));
			}
		}
			
			
		public void SetAsGuestUser()
		{
			UserEmail = string.Empty;
		}

		private EWCAuthentication() 
		{
		}

		public static EWCAuthentication GetInstance()
		{
			return _instance;
		}

		public void Logout(string email)
		{
			UserEmail = "";
			RemoveLocalAccountStores (email);
		//	SocialManager social = new SocialManager ();
			//social.DeleteAllExistingSocialAccounts ();
		}

		public async Task GetEWCGuestProfiles(string email, string token)
		{
			try
			{
				var guest = new EWCGuest ();

				if (!string.IsNullOrEmpty (email)) 
				{
					guest.EWCToken = token;

					if (!string.IsNullOrEmpty (guest.EWCToken)) 
					{
						guest.EWCUserProfile = await GetEWCUserProfile (email, guest.EWCToken);
						guest.AzureUserProfile = await GetAzureProfile (email);
					}
				}

				if (guest.IsValid)
				{
					Util.SaveFullUserProfile (guest);
					AppDelegate.EWCUser = guest;
					EWCApiManager.AddOrUpdateAzureProfile (guest.EWCUserProfile);
				}
			}catch(Exception ex)
			{
				//Loggly.LogToLoggly (JsonConvert.SerializeObject (new Loggly.MessagePacket() {Message = ex.Message}), "CrashReport");
			}

		}

		public async Task<User> GetAzureProfile(string email)
		{
			var response = await NetworkManager.ServiceGet<User> (LocalGlobals.AzureGetGuestProfile + email);
			if (response.Success)
			{
				return response.Result;
			}
			return null;
		}

		User GetMockGuestUserAccount(){
			return new User () { FirstName = "EWC", LastName = "Guest",Email="guest@email.com", ID = 999999 };
		}

		public async Task<GuestProfile> GetEWCUserProfile(string email, string token)
		{
			var response = await EWCApiManager.GetGuestProfile (email, token);

			if (response.Success)
			{
				return response.Payload;
			}
			return null;
		}

		private Dictionary<string,string> GetLocalStorageProps(string email)
		{
//			try{
//
//				KeyChainAccount key= new KeyChainAccount();
//				var accounts = key.FindAccountsForService (APPNAME).ToArray();
//
//				if (accounts.Any())
//				{
//					if (!string.IsNullOrEmpty(email))
//					{
//						var userAccount = accounts.FirstOrDefault (u => u.Username == email);
//						if (userAccount != null) {
//
//							return userAccount.Properties;
//						}
//					}
//				}
//			}catch(Exception ex)
//			{
//				Log.LogError (ex);
//			}

			return null;
		}

		public string GetStoredValue(string email, StorageValueOptions storageOption)
		{
			try{

				var props = GetLocalStorageProps(email);
				if (props != null && props.Any ())
				{
					string propVal;
					props.TryGetValue (storageOption.ToString (), out propVal);
					if (!string.IsNullOrEmpty (propVal))
					{
						return propVal;
					}

				}
			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("GetStoredValue",ex.Message.ToString());
			}

			return string.Empty;
		}


		public void SaveStoredValue(string email,  StorageValueOptions storageOption, string value)
		{
			try
			{
				Dictionary<string,string> settings = new Dictionary<string,string>();

				var props = GetLocalStorageProps(email);

				if (props != null)
				{
					settings = props;
				}

				string propVal;
				settings.TryGetValue (storageOption.ToString (), out propVal);
				if (string.IsNullOrEmpty (propVal))
				{
					settings.Add(storageOption.ToString (), value);
				}else
				{
					settings[storageOption.ToString ()] = value;
				}

				AccountManager.SaveAccount(email, APPNAME, settings);

			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("SaveStoredValue",ex.Message.ToString());
			}
		}

		public void RemoveLocalAccountStores(string email)
		{
			try{
//				KeyChainAccount key= new KeyChainAccount();
//
//				var accounts = key.FindAccountsForService (APPNAME).ToArray();
//
//				if (accounts.Any())
//				{
//					if (!string.IsNullOrEmpty(email))
//					{
//						var userAccount = accounts.FirstOrDefault (u => u.Username == email);
//						if (userAccount != null) {
//							key.Delete(userAccount,APPNAME);
//						}
//					}
//				}

			}catch(Exception ex)
			{
				//Log.LogError (ex);
				Log.Error ("RemoveLocalAccountStores",ex.Message.ToString());
			}
		}
	}
}

