using System;
using Android.Locations;
//using MonoTouch.CoreLocation;
//using MonoTouch.Foundation;
//using MonoTouch.UIKit;
using EWCAndroid.Models;
//using DataModel.Models.Entity;

namespace EWCAndroid.Helpers
{
	public class CoreLocationManager// : CLLocationManagerDelegate
	{
		public CoreLocationManager(){}

		public override void RegionEntered (CLLocationManager manager, CLRegion region)
		{
			DateTime lastEntered;

			try{
			//if entering a center
			if(region.Identifier == LocalGlobals.WaxCenterEntry)
			{
				bool sendBeaconEnterPush = true;
				var lastDate = Helpers.Configuration.GetStringStoredValue (LocalGlobals.LastEnteredCenterDate);

				//if  entered before
				if (!string.IsNullOrEmpty(lastDate))
				{
					lastEntered = Convert.ToDateTime (lastDate);

					//its been less than 2 hours since you last entered the region?
					if (DateTime.Now < lastEntered.AddHours(2))
					{
						sendBeaconEnterPush = false;
					}
				}

				if(sendBeaconEnterPush)
				{
					Helpers.Configuration.SetStringStoredValue (LocalGlobals.LastEnteredCenterDate, DateTime.Now.ToString ());
					NotificationHelper.SendLocalAlertNotification (LocalGlobals.PushTitle, AppDelegate.AppContent.ServerContent [Globals.EntryBeaconMessage].ToString(), LocalGlobals.HelloGorgeousSound);
				}
				return;
			}


			//if entering a geofence around home store
			if(region.Identifier == LocalGlobals.GeofenceEntry)
			{
				bool sendGeoEnterPush = true;
				var lastDate = Helpers.Configuration.GetStringStoredValue (LocalGlobals.LastEnteredGeoFenceDate);

				//if  entered before
				if (!string.IsNullOrEmpty(lastDate))
				{
					lastEntered = Convert.ToDateTime (lastDate);

						//its been less than x days since you last entered the region
					if (DateTime.Now < lastEntered.AddDays(10))
					{
						sendGeoEnterPush = false;
					}
				}

				if(sendGeoEnterPush)
				{
					Helpers.Configuration.SetStringStoredValue (LocalGlobals.LastEnteredGeoFenceDate, DateTime.Now.ToString ());
					var fenceMsg = AppDelegate.AppContent.ServerContent [LocalGlobals.GeofenceEntry].ToString ();
					if (fenceMsg != "None") {
						NotificationHelper.SendLocalAlertNotification (LocalGlobals.PushTitle, AppDelegate.AppContent.ServerContent [LocalGlobals.GeofenceEntry].ToString (), "");
					}
				}
				return;
			}
			}catch(Exception ex)
			{
				Log.LogError (ex);
			}

		}

		public override void RegionLeft (CLLocationManager manager, CLRegion region)
		{
			manager.StopRangingBeacons (region as CLBeaconRegion);
		}
	}
}
