using System;
using DataModel.Models.Entity;
using EWCAndroid.EWCAPI.Models;

namespace EWCAndroid
{
	public class EWCGuest
	{

		public User AzureUserProfile {get; set;}
		public GuestProfile EWCUserProfile { get; set;}
		public string EWCToken { get; set;}
		public bool IsValid {
			get{
				if (AzureUserProfile == null || EWCUserProfile == null)
					return false;
				
				return true;
			}
		}
	}
}

