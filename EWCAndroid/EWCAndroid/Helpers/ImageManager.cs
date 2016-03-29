using System;
using Mono;
//using MonoTouch.UIKit;
//using MonoTouch.Foundation;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
//using ModernHttpClient;
//using MonoTouch.AVFoundation;
//using MonoTouch.CoreMedia;
//using MonoTouch.CoreGraphics;
using System.IO;
using Android.Webkit;

namespace EWCAndroid.Helpers
{
	public static class ImageManager
	{
//		public static UIImage FromUrl (string uri)
		public static WebViewClient FromUrl (string uri)
		{
			UIImage image = null;
			try {
					using (var url = new NSUrl (uri)) {
						using (var data = NSData.FromUrl (url)) {
							if (data != null) {
								image = UIImage.LoadFromData (data);
							}
						}
					}
			} catch (Exception ex) {
				Log.LogError (ex);
			}
			return  image;
		}

		//public static UIImage ImageFor (string url, int time = 5)
		public static WebViewClient ImageFor (string url, int time = 5)
		{
			AVUrlAssetOptions avOption = new AVUrlAssetOptions ();

			AVUrlAsset avAsset = new AVUrlAsset (new MonoTouch.Foundation.NSUrl (url),avOption);

			AVAssetImageGenerator imageGenerator = AVAssetImageGenerator.FromAsset(avAsset);
			imageGenerator.AppliesPreferredTrackTransform = true;

			CMTime requestedTime = avAsset.Duration;

			requestedTime.Value = requestedTime.TimeScale * time;

			CMTime actualTime;
			NSError error = null;

			using (CGImage posterImage = imageGenerator.CopyCGImageAtTime (requestedTime, out actualTime, out error)) {
				if (error == null) {
					var img = UIImage.FromImage (posterImage);
					if (img != null){
						return ImageManager.MaxResizeImage (img, 89f, 50f);
					}
				}
				return ImageManager.MaxResizeImage (UIImage.FromBundle ("videoPlaceholder"), 89f, 50f);;
			}
		}


//		public static async Task<UIImage> LoadImageFromUrl (string imageUrl)
		public static async Task<WebViewClient> LoadImageFromUrl (string imageUrl)
		{
			UIImage image = null;
			try{
				if(NetworkManager.IsOnline)
				{
						using (var httpClient = new HttpClient (new NativeMessageHandler ())) {
							byte[] contents = await httpClient.GetByteArrayAsync (imageUrl);
							image =  UIImage.LoadFromData (NSData.FromArray (contents));
						}
				}
			}
			catch(Exception ex)
			{
				Log.LogError(ex);
			}
			return image;
		}

//		public static string ImageTo64String(UIImage image)
		public static string ImageTo64String(WebViewClient image)
		{
			NSData img = image.AsPNG ();
			return img.GetBase64EncodedData (NSDataBase64EncodingOptions.None).ToString ();
		}
//		public static UIImage FromBase64String(string base64image)
		public static WebViewClient FromBase64String(string base64image)
		{
			if (base64image == null){
				return null;
			}
				
			using (var data = NSData.FromArray (Convert.FromBase64String( base64image))) {
				return  UIImage.LoadFromData (data);
			};
		}

		// resize the image to be contained within a maximum width and height, keeping aspect ratio
//		public static UIImage MaxResizeImage(UIImage sourceImage, float maxWidth, float maxHeight)
		public static WebViewClient MaxResizeImage(WebViewClient sourceImage, float maxWidth, float maxHeight)
		{
			var sourceSize = sourceImage.Size;
			var maxResizeFactor = Math.Max(maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);
			if (maxResizeFactor > 1) return sourceImage;
			var width = maxResizeFactor * sourceSize.Width;
			var height = maxResizeFactor * sourceSize.Height;
			UIGraphics.BeginImageContext(new SizeF(width, height));
			sourceImage.Draw(new RectangleF(0, 0, width, height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return resultImage;
		}
	}
}

