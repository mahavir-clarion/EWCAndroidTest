using System;
//using MonoTouch.CoreLocation;
//using MonoTouch.Foundation;

namespace EWCAndroid
{
	public class LocationServicesHelper
	{
		public Action<Location> ReverseGeoUpdate;

		public void ReverseGeocodeLocationHandle (CLPlacemark[] placemarks, NSError error)
		{
			Location loc = new Location ();

			try{
				if (error == null){
					if (placemarks != null && placemarks.Length > 0) {
						for (int i = 0; i < placemarks.Length; i++) {
							loc.City = placemarks [i].Locality;
							loc.State = placemarks [i].AdministrativeArea;
							loc.GeoCode = placemarks [i].Location.Coordinate.Latitude + ":" + placemarks [i].Location.Coordinate.Longitude;
							loc.Coordinate = placemarks [i].Location.Coordinate;
						}
					}
				}
			}catch (Exception ex)
			{
				Log.LogError (ex);
				Log.LogInfo ((error != null) ? error.DebugDescription.ToString () : "");
			}

			ReverseGeoUpdate(loc);
		}
	}
}

