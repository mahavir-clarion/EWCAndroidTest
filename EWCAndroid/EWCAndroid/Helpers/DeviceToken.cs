
using System;
//using MonoTouch.Security;
//using MonoTouch.Foundation;

namespace EWCAndroid.Helpers
{
	//https://github.com/Redth/PushSharp/blob/master/Client.Samples/PushSharp.ClientSample.MonoTouch/PushSharp.ClientSample.MonoTouch/AppDelegate.cs
	public static class DeviceToken
	{
		public static string SetToken(NSData deviceToken)
		{
		
			// The deviceToken is what your push notification server needs to send out a notification
			// to the device.  So you want to send the device Token to your servers when it has changed.
			// The device token can change: http://stackoverflow.com/questions/6652242/does-the-device-token-ever-change-once-created
			// 'If the user restores backup data to a new device or reinstalls the operating system, the device token changes.'
			// The deviceToken is not the same as the Device ID: http://stackoverflow.com/questions/3726107/what-is-the-difference-between-iphone-device-udid-iphone-device-id-and-iphone

			//First, get the last device token we know of. If this is the first time we come here, this will be empty.
			string lastDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("deviceToken");

//			var strFormat = new NSString("%@");
//			var dt = new NSString(MonoTouch.ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr_IntPtr(new MonoTouch.ObjCRuntime.Class("NSString").Handle, new MonoTouch.ObjCRuntime.Selector("stringWithFormat:").Handle, strFormat.Handle, deviceToken.Handle));
 //			var newDeviceToken = dt.ToString().Replace("&lt;", "").Replace("&gt;", "").Replace(" ", "");
//
			//We only want to send the device token to the server if it hasn't changed since last time
			// no need to incur extra bandwidth by sending the device token every time.
			// If this is the first time we come here, the "lastDeviceToken" will be empty, so then the deviceToken will be set.
			if (!deviceToken.ToString().Equals (lastDeviceToken)) {
				//Save the new device token for next application launch
				NSUserDefaults.StandardUserDefaults.SetString (deviceToken.ToString(), "deviceToken");

				//If you want to let the server know about the devicetoken, do it here.
				//You can also do it at the moment you have contact with the service, for example when
				//you want to let the server do some kind of action.

				return deviceToken.ToString();
			}
			else
			{
				return lastDeviceToken;
			}
		}

		public static string RetrieveToken()
		{
			string lastDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("deviceToken");

			return lastDeviceToken;
		}

	}
}
