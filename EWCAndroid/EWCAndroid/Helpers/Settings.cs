using System;
//using Foundation;
using Android.Preferences;
using Android.Content;
using System.Linq.Expressions;
namespace EWCAndroid.Helpers
{
	public static class Configuration
	{
		static Context AppContext { get; set; }

		public static void Init(Context ctx) {
			AppContext = ctx;
		}

	

		static public string GetStringStoredValue(string key)
		{
			//string value = PreferenceManager.GetDefaultSharedPreferences (AppContext).GetString (key, "");
			//return value != null ? (T)Convert.ChangeType (value, typeof(T)) : default(T);

			string value = PreferenceManager.GetDefaultSharedPreferences(AppContext).GetString (key, "");
			//string value = NSUserDefaults.StandardUserDefaults.StringForKey(key); 
			if (string.IsNullOrEmpty(value))
				return string.Empty;
			else
				return value;
		}

		public static int GetIntStoredValue(string key)
		{
			//return NSUserDefaults.StandardUserDefaults.IntForKey(key); 
			return PreferenceManager.GetDefaultSharedPreferences(AppContext).GetInt(key,0);
		}

		public static bool GetBoolStoredValue(string key)
		{
			return PreferenceManager.GetDefaultSharedPreferences(AppContext).GetBoolean(key,true);
			//return NSUserDefaults.StandardUserDefaults.BoolForKey(key); 
		}

		public static void SetStringStoredValue(string key, string value)
		{
//			NSUserDefaults.StandardUserDefaults.SetString(value, key.ToString ()); 
//			NSUserDefaults.StandardUserDefaults.Synchronize ();


			var editor = PreferenceManager.GetDefaultSharedPreferences(AppContext).Edit();
			editor.PutString(key, value.ToString());
			editor.Commit();
		}

		public static void SetIntStoredValue(string key, int value)
		{
//			NSUserDefaults.StandardUserDefaults.SetInt(value, key.ToString ()); 
//			NSUserDefaults.StandardUserDefaults.Synchronize ();

			var editor = PreferenceManager.GetDefaultSharedPreferences(AppContext).Edit();
			editor.PutInt(key, value);
			editor.Commit();
		}


		public static void SetBoolStoredValue(string key, bool value)
		{
//			NSUserDefaults.StandardUserDefaults.SetBool(value, key.ToString ()); 
//			NSUserDefaults.StandardUserDefaults.Synchronize ();

			var editor = PreferenceManager.GetDefaultSharedPreferences(AppContext).Edit();
			editor.PutBoolean(key, value);
			editor.Commit();
		}
	}
}
