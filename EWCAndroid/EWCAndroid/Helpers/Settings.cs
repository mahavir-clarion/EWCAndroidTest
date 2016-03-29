using System;
//using MonoTouch.Foundation;

namespace EWCAndroid.Helpers
{
	public static class Configuration
	{
		static public string GetStringStoredValue(string key)
		{
			string value = NSUserDefaults.StandardUserDefaults.StringForKey(key); 
			if (string.IsNullOrEmpty(value))
				return string.Empty;
			else
				return value;
		}

		public static int GetIntStoredValue(string key)
		{
			return NSUserDefaults.StandardUserDefaults.IntForKey(key); 
		}

		public static bool GetBoolStoredValue(string key)
		{
			return NSUserDefaults.StandardUserDefaults.BoolForKey(key); 
		}

		public static void SetStringStoredValue(string key, string value)
		{
			NSUserDefaults.StandardUserDefaults.SetString(value, key.ToString ()); 
			NSUserDefaults.StandardUserDefaults.Synchronize ();
		}

		public static void SetIntStoredValue(string key, int value)
		{
			NSUserDefaults.StandardUserDefaults.SetInt(value, key.ToString ()); 
			NSUserDefaults.StandardUserDefaults.Synchronize ();
		}


		public static void SetBoolStoredValue(string key, bool value)
		{
			NSUserDefaults.StandardUserDefaults.SetBool(value, key.ToString ()); 
			NSUserDefaults.StandardUserDefaults.Synchronize ();
		}
	}
}
