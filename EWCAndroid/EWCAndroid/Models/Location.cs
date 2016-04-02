using System;
using Android.Locations;
//using MonoTouch.CoreLocation;
using Mono;

namespace EWCAndroid
{
	public class Location
	{
		public string City { get; set; }
		public string State { get; set; }
		public string Zipcode { get; set; }
		public string GeoCode { get; set; }
//		public CLLocationCoordinate2D Coordinate { get; set; }
		public LocationManager Coordinate { get; set; }
	}
}

