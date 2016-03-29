using System;
using Mono;
using Android.Util;
using Android.Webkit;

//using MonoTouch.UIKit;
//using MonoTouch.Foundation;

namespace EWCAndroid.Helpers
{
	public class WebViewDelegate// : UIWebViewDelegate
	{
		public WebViewDelegate ()
		{
		}
		public override bool ShouldStartLoad (WebViewClient webView, NSUrlRequest request, WebViewClientNavigationType navigationType)
		{
			return true;
		}

		public override void LoadingFinished (WebViewClient webView)
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
		}

		public override void LoadStarted (WebViewClient webView)
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
		}

		public override void LoadFailed (WebViewClient webView, NSError error)
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
		}

	}
}

