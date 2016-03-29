//using MonoTouch.CoreLocation;
//using MonoTouch.Foundation;


namespace EWCAndroid.Helpers
{
	public class BeaconRegion
	{
		public CLBeaconRegion Region { get; private set;}

		public BeaconRegion(string uuid, string regionName)
		{
			Region = new CLBeaconRegion (new NSUuid (uuid),LocalGlobals.WaxCenterEntryMajor,regionName);
			Region.NotifyEntryStateOnDisplay = true;
			Region.NotifyOnEntry = true;
			Region.NotifyOnExit = true;
		}
	}
}
