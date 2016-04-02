using System;
using Xamarin.Auth;
using System.Collections.Generic;


namespace EWCAndroid.Helpers
{
	public static class AccountManager
	{
		public static void SaveAccount(string userId,string accountName, Dictionary<string,string> accountProps)
		{
			var account = new Account (userId, accountProps);
			//KeyChain.KeyChainAccount key= new KeyChain.KeyChainAccount();
			//key.Save(account, accountName);
		}

	}
}

