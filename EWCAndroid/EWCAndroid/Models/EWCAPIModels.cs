using System;
using System.Net;
using System.Collections.Generic;
using Xamarin.Auth;

namespace EWCAndroid.EWCAPI.Models
{
	public class GuestProfile
	{
		public string Email { get; set; }
		public int GlobalId { get; set; }
		public int LocalId { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Gender { get; set; }
		public string PhoneNumber { get; set; }
		public string StreetAddress1 { get; set; }
		public string StreetAddress2 { get; set; }
		public bool IsRestricted { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public DateTime OnlineUserSince { get; set; }
		public DateTime? MemberSince { get; set; }
		public int Points { get; set; }
		public List<Package> Packages { get; set; }
	}

	public class NewGuestRequestResponse
	{
		public int Global_Id { get; set; }
		public string LId { get; set; }
		public string First { get; set; }
		public string Last { get; set; }
		public PhoneNumber[] Phnums { get; set; }
	}


	public class PhoneNumber
	{
		public string Homephone { get; set; }
		public string Busphone { get; set; }
		public string Cellphone { get; set; }
	}


	public class NewGuestRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public bool BeenToEwcBefore { get; set; }
		public int BookingLocationId { get; set; }
		public string Gender { get; set; }
		public int CreateOverride { get; set; }
		public int GlobalId { get; set; }
		public string Password { get; set; }
		public string Request_Channel { get; set; }

		public NewGuestRequest(){
			CreateOverride = 0;
			BeenToEwcBefore = true;
			Request_Channel = "iOS";
		}
	}

	public class UpdateGuestProfileRequest
	{
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Email { get; set; }
		public string Gender { get; set; }
		public string PhoneNumber { get; set; }
		public string StreetAddress1 { get; set; }
		public string StreetAddress2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public int LocalID { get; set; }
		public bool PlainTextEmail { get; set; }
	}


	public class UpdatePasswordRequest
	{
		public string Email { get; set; }
		public string CurrentPassword { get; set; }
		public string NewPassword { get; set; }
	}

	public class AuthenticationResponse
	{
		public string BearerToken { get; set; }
		public string UserName { get; set; }
	}

	public class UpdateEmailRequest
	{
		public string CurrentEmail { get; set; }
		public string NewEmail{ get; set; }
		public string Password { get; set; }
	}


	public class ForgotPasswordRequest
	{
		public string Email{ get; set; }
	}

	public class Package
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime? ExpirationDate { get; set; }
		public int? RemainingVisits { get; set; }
		public int? TotalVisits { get; set; }
		public bool IsUnlimited { get; set; }
		public string ServiceCode { get; set; }
		public string ServiceDescription { get; set; }
	}


	public class GuestPoints
	{
		public int Points { get; set; }
	}

	public class EWCServiceResponse<T> : EWCBaseResponse
	{
		public T Payload { get; set; }
	}

	public abstract class EWCBaseResponse
	{
		public bool Success { get; set; }
		public string ErrorMessage { get; set; }
		public HttpStatusCode ErrorCode { get; set; }
	}
}

