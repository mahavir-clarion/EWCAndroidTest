using System;

namespace EWCAndroid
{

	enum ShareOptions
	{
		Facebook,
		Twitter,
		SMS,
		Mail
	}
	public static class LocalGlobals
	{

		public static string IOSClientSecret
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "9DE63D2E72F54AA3BDFD8CE867AEBD78";
				//}
				//return "BF6FDCC0ED4045519BBAAC49123667CD";
			}
		}

		public static string IOSClientID
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "ios_client";
				//}
				//return "ios_client";
			}
		}

		public static string EWCAPIRoot
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "https://dev-ewcoauth.azurewebsites.net/";
				//}
				//return "https://ewcoauth.azurewebsites.net/";
			}
		}

		public static string AMSAppId
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "mshzEWkSpeTUlssAkdlajDxuKxuiaH28";
				//}
				//return "ilMlhCPlbOkCoawixtvrwIcaGUnfdF82";
			}
		}

		public static string AMSRootURL
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "https://dev-waxcenter.azure-mobile.net/";
				//}
				//return "https://waxcenter.azure-mobile.net/";
			}
		}

		public static string HockeyAppId
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "79a5cb4abd1af75355e4cfd58663dc21";
				//}
				//return "292851d391e88b035d8591d3157586f3";
			}
		}

		public static string FacebookAppID
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "653881064721674";
				//}
				//return "1573836846178331";
			}
		}

		public static string FacebookSecret
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "ee07a3316d55bbebe1b21b6938aa6edd";
				//}
				//return "a52d6ebc1fcf7a67abe04c55554a188b";
			}
		}

		public const string FacebookAuthorizeURL = "https://m.facebook.com/dialog/oauth/";

		public static string FbDisplayName
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "European Wax Center Mobile Dev";
				//}
				//return "European Wax Center Mobile";
			}
		}

		//		public static string FacebookRedirectURL
		//		{	get
		//			{
		//				if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
		//				{
		//					return "https://dev-waxcenter.azure-mobile.net/signin-facebook";
		//				}
		//				return  "https://waxcenter.azure-mobile.net/signin-facebook";
		//			}
		//		}

		public static string FacebookRedirectURL
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "https://dev-ewcweb.azurewebsites.net/socialredirect/redirect.html";
				//}
				//return  "https://ewcweb.azurewebsites.net/socialredirect/redirect.html";
			}
		}

		public const string TwitterRequestTokenURL = "https://api.twitter.com/oauth/request_token";
		public const string TwitterAuthorizeURL = "https://api.twitter.com/oauth/authorize";
		public const string TwitterAccessTokenURL = "https://api.twitter.com/oauth/access_token";


		public static string TwitterRedirectURL
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "https://dev-ewcweb.azurewebsites.net/socialredirect/redirect.html";
				//}
				//return "https://ewcweb.azurewebsites.net/socialredirect/redirect.html";
			}
		}

		public static string TwitterAPIKey
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "SOBneY08lyIEzNGlJy2SbajwW";
				//}
				//return "2TkyWSWHpvKy2S5oFJoeqkM79";
			}
		}

		public static string TwitterAPISecret
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "VhNqe9IJz5e6N7f7esLAOV1Uq6CA438lCRsXZlxMy943Ff6bVk";
				//}
				//return "r83V1cORwHLObzVejEP82Fd7ftvIOgGE97TVsjBFTsVIHi4MAe";
			}
		}


		public const string GoogleAuthorizeURL = "https://accounts.google.com/o/oauth2/auth";
		public const string GoogleAccessTokenURL = "https://accounts.google.com/o/oauth2/token";


		public static string GoogleRedirectURL
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "https://dev-ewcweb.azurewebsites.net/socialredirect/redirect.html";
				//}
				//return "https://ewcweb.azurewebsites.net/socialredirect/redirect.html";
			}
		}

		public static string GoogleAPIKey
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "AIzaSyBwlEIeZMriROc5RseOvIKneQRLNmrPyfE";
				//}
				//return "AIzaSyBwlEIeZMriROc5RseOvIKneQRLNmrPyfE";
			}
		}

		public static string GoogleClientID
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "281333954638-r0it4krmm3ch117ft0q04k653hqkjjn6.apps.googleusercontent.com";
				//}
				//return "281333954638-r0it4krmm3ch117ft0q04k653hqkjjn6.apps.googleusercontent.com";
			}
		}

		public static string GoogleAPISecret
		{	get
			{
				//if ( NSBundle.MainBundle.InfoDictionary ["CFBundleIdentifier"].ToString().Contains("dev"))
				//{
					return "lZUjFPGS7bHNaZIeCQYHkSea";
				//}
				//return "lZUjFPGS7bHNaZIeCQYHkSea";
			}
		}


		//Values
		public const string TwitterAccountServiceID = "Twitter";
		public const string FacebookAccountServiceID = "Facebook";
		public const string GoogleAccountServiceID = "Google";

		public static string UpdateEmailAndName = "user/updateuseremailname";
		public static string LoginEndPoint = "authentication/login";
		public static string ProfilePicEndPoint = "user/image";
		public static string UserEndPoint = "user/";
		public static string EmailUpdateEndPoint = "user/email/";
		public static string EmailAddressUpdateEndPoint = "user/updateemail";
		public static string CentersEndPoint = "location/centersbycoordinates";
		public static string AllCentersEndPoint = "location/centers"; 
		public static string AllSiteCoreCentersEndPoint = "location/sitecorecenters"; 
		public static string FacebookUserFriends = "user/facebookuserfriends";
		public static string HomeCenterUpdateEndPoint = "user/homecenter/";
		public static string UserUpdateEndPoint = "user/update"; //gender only for now
		public static string RateEndPoint = "user/rate/";
		public static string LoggingEndPoint = "log/";
		public static string AzureStatusURL = "https://waxcenter.azure-mobile.net/api/services/status";
		public static string AzureReqMinVerURL = "https://waxcenter.azure-mobile.net/api/services/versionrequired";
		public static string AppContentEndPoint = "appcontent/current";
		public static string MainImageEndPoint = "appcontent/currentimage";
		public static string AllBeaconContent = "appcontent/allbeaconcontent";
		public static string BeaconContent = "appcontent/beacon";
		public static string FAQEndPoint = "appcontent/currentfaqs";
		public static string SocialVideoEndPoint = "appcontent/currentvideos";
		public static string SignalRArrivalHub = "arrivalhub/registerarrival";

		//depcreated in 2.5
		public static string UpdateResRelatedUserData = "user/UpdateFromResRequest";



		public static string ResGetServices = "waxservice/sclocation/";
		public static string ResWaxersAndFirstOpenDates = "waxservice/scwaxersanddates";
		public static string ResAllWaxersOpenTimes = "waxservice/scallwaxeropentimes";
		public static string ResWaxerOpenDates = "waxservice/scwaxeropendates";
		public static string ResWaxerOpenTimes = "waxservice/scwaxeropentimes";
		public static string ResFutureReservations = "waxservice/scguestfurtureres";
		public static string ResCreateRes = "waxservice/reservation/sccreateres";
		public static string ResCreateGuestProfile = "waxservice/user/scprofile";
		public static string ResCenterStatus = "waxservice/sccenterstatus";
		public static string ResGuestSearch= "waxservice/scguestsearch";
		public static string ResCenterLookup= "waxservice/sccenterlookup";

		public static string BeaconLggger= "beaconlogger/logentry";


		public static string ReservationURL = "http://www.waxcenter.com/reservations";
		public static string HomePage = "http://www.waxcenter.com";
		public static string TipsURL = "http://www.waxcenter.com/products";
		public static string TermsURL = "http://www.waxcenter.com/terms-conditions";


		public static string EWCUUID = "505899BC-A54F-4B32-A2AC-8F066081726F";

		public static ushort WaxCenterEntryMajor = 1000;
		public static string WaxCenterEntryRegionName = "WaxCenterEntry";
		public static ushort WaxCenterSpecialEventMajor = 2000;
		public static string WaxCenterSpecialEventRegionName = "SpecialEvent";

		//nsdefaults
		public static string LastEnteredCenterDate= "LastEnteredCenterDate";
		public static string LastEnteredGeoFenceDate ="LastEnteredGeoFenceDate";

		public static string PushTitle ="EWC";
		public static string EntryBeaconMessage = "Hello Gorgeous!";
		public static string EntryGeofenceMessage = "EntryGeofenceMessage";
		public static string HelloGorgeousSound = "hellogorgeous.wav";
		public static string StrutWelcomeSound = "MelanieSound.wav";


		public static string WifiDisabledMessage = "Cellular/Wifi are not available. Please check your network settings and/or turn Airplane Mode off.";
		public static string NoCentersFoundMessage = "We don't appear to have an EWC near that location...not yet :)";
		public static string CenterUnAvailable = "Sorry Gorgeous, this Center is not available at the moment.   Please select another Center or tap the call icon of your favorite Center for more assistance.";
		public static string CenterUnAvailable2 = "Sorry Gorgeous, this Center is not available at the moment.   Please select another Center or call your favorite Center for more assistance.";


		//New Azure endpoint for new Auth 2.5
		public static string UserProfileEndPoint = "user/profile/";


		//New EWC API Endpoints v 2.5

		public static string EWCAPIPostGetToken = "token";

		public static string EWCAPIGetGuestProfile = "api/guests";

		public static string EWCAPIPostCreateGuest = "api/guests";

		public static string EWCAPIPutUpdateGuest = "api/guests";

		public static string EWCAPIPutUpdatePassword = "api/guests/ChangePassword";

		public static string EWCAPIPutUpdateEmail = "api/guests/UpdateEmail";

		public static string EWCAPIPostForgotPassword = "api/guests/ForgotPassword";

		public static string EWCAPIGetPoints = "api/points";

		public static string EWCAPIGetPackages = "api/packages";


		//Azure
		public static string AzurePostAddOrUpdateGuest = "user/v2/guest";
		public static string AzureGetGuestProfile = "user/v2/profile/";
		public static string AzurePostUpdateUserHomeCenter = "user/v2/homecenter";
		public static string AzurePostUserRating = "user/v2/rate";
	}
}
