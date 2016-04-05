
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using DataModel.Models.Entity;

namespace EWCAndroid
{
	//[Activity (Label = "FaqView")]	
	[Activity (Label = "EWCAndroid", MainLauncher = false, Icon = "@mipmap/icon")]
	public class FaqView : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			GetFAQs ();
		}
		async Task GetFAQs ()
		{
			//BTProgressHUD.Show ("");

			var response = await NetworkManager.ServiceGet<List<FAQModel>>(LocalGlobals.FAQEndPoint);

			if (response.Success)
			{
				//this.tableView.Source = new FAQDataSource (response.Result);
			}
			//this.tableView.ReloadData ();
			//BTProgressHUD.Dismiss ();
		}
	}
}

