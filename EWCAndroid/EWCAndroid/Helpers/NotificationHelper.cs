using System;
//using MonoTouch.Foundation;
//using MonoTouch.UIKit;
using Mono;
using Android.Util;
using System.Linq;

namespace EWCAndroid.Helpers
{
	public static class NotificationHelper
	{
		public static void SendLocalAlertNotification (string title, string msg, string soundName, int applicationIconBadgeNumber = 1, int delayInSeconds = 1)
		{
			// create the notification
			var notification = new UILocalNotification ();
			var keyName = title;

			//The Name can be used to cancel a Local notifications
			var keys = new object[] { keyName };
			var objects = new object[] { keyName};
			var userInfo = NSDictionary.FromObjectsAndKeys (objects, keys);


			// set the fire date/time
			notification.FireDate = DateTime.Now.AddSeconds (delayInSeconds);
			// configure the alert stuff
			notification.AlertAction = title;
			notification.AlertBody = msg;
			notification.UserInfo = userInfo;

			// modify the badge
			notification.ApplicationIconBadgeNumber = applicationIconBadgeNumber;


			// set the sound to be the default sound
			if (string.IsNullOrEmpty(soundName))
			{
				notification.SoundName = UILocalNotification.DefaultSoundName;
			}else
			{
				notification.SoundName = soundName;
			}

			// schedule
			UIApplication.SharedApplication.ScheduleLocalNotification (notification);
		}

//		public static void SendLocalAlertNotification (BeaconContent message, int delayInSeconds = 1)
//		{
//			// create the notification
//			var notification = new UILocalNotification ();
//			var keyName = message.NotificationTitle;
//
//			// set the fire date/time
//			notification.FireDate = DateTime.Now.AddSeconds (delayInSeconds);
//			// configure the alert stuff
//			notification.AlertAction = message.NotificationTitle;
//			notification.AlertBody = message.NotificationMessage.ToString();
//
//			// modify the badge
//			notification.ApplicationIconBadgeNumber += 1;
//
//
//			// set the sound to be the default sound
//			notification.SoundName = UILocalNotification.DefaultSoundName;
//
//			// schedule it
//			UIApplication.SharedApplication.ScheduleLocalNotification (notification);
//		}

	}
}

