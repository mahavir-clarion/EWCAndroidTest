using System;

namespace EWCAndroid.GoogleResult
{
		public class GoogleRoot
		{
			public Result[] Results { get; set; }
			public string Status { get; set; }
		}

		public class Result
		{
			public string Formatted_address { get; set; }
			public Geometry Geometry { get; set; }
			public string[] Postcode_localities { get; set; }
			public string[] Types { get; set; }
		}

		public class Geometry
		{
			public Location Location { get; set; }
			public string Location_type { get; set; }
		}

		public class Location
		{
			public float Lat { get; set; }
			public float Lng { get; set; }
		}

}

