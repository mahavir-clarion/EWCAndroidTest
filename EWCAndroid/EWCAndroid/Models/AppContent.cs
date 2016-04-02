using System;
using Newtonsoft.Json.Linq;
//using MonoTouch.UIKit;
//using EWCiOS.Helpers;
using System.Threading.Tasks;
using Android.Util;
using Android.Webkit;

namespace EWCAndroid
{
	public class Content
	{

		private static Content _sharedInstance = null;
		private static readonly object padlock = new object();
		private JObject _content;

		public JObject ServerContent {
			get
			{
				if (_content == null) {
					var localContent = Helpers.Configuration.GetStringStoredValue ("AppContent");
					if(!string.IsNullOrEmpty(localContent))
					{
						_content = JObject.Parse(localContent);
					}
				}
				return _content;
			}
			set{
				_content = value;
				Helpers.Configuration.SetStringStoredValue ("AppContent", _content.ToString ());
			}
		}

		Content()
		{

		}
		public static Content ContentInstance
		{
			get 
			{
				lock (padlock) 
				{
					if (_sharedInstance == null) {
						_sharedInstance = new Content ();
					}
					return _sharedInstance;
				}
			}

		}

		public async Task<WebViewClient> GetImageFromProperty(string image)
		//public async Task<UIImage> GetImageFromProperty(string image)
		{
			if (this.ServerContent != null) {
				var prop = this.ServerContent [image];
				if (prop != null) {
					return await ImageManager.LoadImageFromUrl (prop.ToString ());
				}
			}
			return null;
		}

		public string StringFromProperty(string value)
		{
			if (this.ServerContent != null) {
				var prop = this.ServerContent [value];
				if (prop != null) {
					return prop.ToString ();
				}
			}
			return string.Empty;
		}
	}
}

