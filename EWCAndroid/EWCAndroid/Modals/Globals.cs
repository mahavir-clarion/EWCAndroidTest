//using System;
////using MonoTouch.Foundation;
//using Mono;
//
//namespace EWCAndroid
//{
//
//	enum ShareOptions
//	{
//		Facebook,
//		Twitter,
//		SMS,
//		Mail
//	}
//	public static class LocalGlobals
//	{
//		public static string AMSAppId
//		{	get
//			{
//				if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
//				{
//					return "mshzEWkSpeTUlssAkdlajDxuKxuiaH28";
//				}
//				return "ilMlhCPlbOkCoawixtvrwIcaGUnfdF82";
//			}
//		}
//
//		public static string AMSRootURL
//		{	get
//			{
//				if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
//				{
//					return "https://dev-waxcenter.azure-mobile.net/";
//				}
//				return "https://waxcenter.azure-mobile.net/";
//			}
//		}
//
//		public static string HockeyAppId
//		{	get
//			{
//				if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
//				{
//					return "79a5cb4abd1af75355e4cfd58663dc21";
//				}
//				return "292851d391e88b035d8591d3157586f3";
//			}
//		}
//
//		public static string FbAppId
//		{	get
//			{
//				if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
//				{
//					return "653881064721674";
//				}
//				return "1573836846178331";
//			}
//		}
//
//		public static string FbDisplayName
//		{	get
//			{
//				if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
//				{
//					return "European Wax Center Mobile Dev";
//				}
//				return "European Wax Center Mobile";
//			}
//		}
//		//Values
//
//		public static string LoginEndPoint = "authentication/login";
//		public static string ProfilePicEndPoint = "user/image";
//		public static string UserEndPoint = "user/";
//		public static string EmailUpdateEndPoint = "user/email/";
//		public static string CentersEndPoint = "location/centersbycoordinates";
//		public static string AllCentersEndPoint = "location/centers";
//		public static string FacebookUserFriends = "user/facebookuserfriends";
//		public static string HomeCenterUpdateEndPoint = "user/homecenter/";
//		public static string RateEndPoint = "user/rate/";
//		public static string LoggingEndPoint = "log/";
//		public static string AzureStatusURL = "https://waxcenter.azure-mobile.net/api/services/status";
//		public static string AppContentEndPoint = "appcontent/current";
//		public static string MainImageEndPoint = "appcontent/currentimage";
//		public static string FAQEndPoint = "appcontent/currentfaqs";
//		public static string SocialVideoEndPoint = "appcontent/currentvideos";
//
//
//
//		public static string ReservationURL = "http://www.waxcenter.com/reservations";
//		public static string HomePage = "http://www.waxcenter.com";
//		public static string TipsURL = "http://www.waxcenter.com/ewc-experience";
//		public static string TermsURL = "http://www.waxcenter.com/terms-conditions";
//
//
//		public static string EWCUUID = "505899BC-A54F-4B32-A2AC-8F066081726F";
//		//public static string EWCUUID = "dfdfdfdf-9345-9348-5348-ce1238434123";
//
//		public static ushort WaxCenterEntryMajor = 1000;
//		public static string WaxCenterEntry = "WaxCenterEntry";
//		public static string GeofenceEntry = "GeofenceEntry";
//
//		//nsdefaults
//		public static string LastEnteredCenterDate= "LastEnteredCenterDate";
//		public static string LastEnteredGeoFenceDate ="LastEnteredGeoFenceDate";
//
//		public static string PushTitle ="EWC";
//		public static string EntryBeaconMessage = "Hello Gorgeous!";
//		public static string EntryGeofenceMessage = "EntryGeofenceMessage";
//		public static string HelloGorgeousSound = "hellogorgeous.wav";
//
//
//		public static string WifiDisabledMessage = "Cellular/Wifi are not available. Please check your network settings and/or turn Airplane Mode off.";
//		public static string NoCentersFoundMessage = "We don't appear to have an EWC near that zipcode...not yet :)";
//	}
//}
