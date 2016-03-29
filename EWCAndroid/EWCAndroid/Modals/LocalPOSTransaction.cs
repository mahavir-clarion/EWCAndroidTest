using System;
//using MonoTouch.Foundation;
using Mono;
using Android.Util;
namespace EWCAndroid.Models
{
	public class LocalPOSTransaction 
	{
		public string LocationId {get;set;}
		public string TransactionId {get;set;}
		public string Services {get;set;}
		public DateTime TransactionDate {get;set;}
	}
}

