using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace EWCAndroid.Helpers.FourSquare
{
	public class FourSquareLocalSearchResult
	{
		public Meta Meta { get; set; }
		public Response Response { get; set; }
	}

	public class Meta
	{
		public int Code { get; set; }
	}

	public class Response
	{
		public Venue[] Venues { get; set; }
		public Geocode Geocode { get; set; }
	}

	public class Venue
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public Contact Contact { get; set; }
		public Location Location { get; set; }
		public Category[] Categories { get; set; }
		public bool Verified { get; set; }
		public Stats Stats { get; set; }
		public Specials Specials { get; set; }
		public Herenow HereNow { get; set; }
		public string ReferralId { get; set; }
		public string Url { get; set; }
		public string StoreId { get; set; }
		public Venuepage VenuePage { get; set; }
		public Menu Menu { get; set; }
	}

	public class Contact
	{
		public string Phone { get; set; }
		public string FormattedPhone { get; set; }
		public string Twitter { get; set; }
	}

	public class Location
	{
		public string Address { get; set; }
		public string CrossStreet { get; set; }
		public float Lat { get; set; }
		public float Lng { get; set; }
		public int Distance { get; set; }
		public string PostalCode { get; set; }
		public string Cc { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public string[] FormattedAddress { get; set; }
		public bool IsFuzzed { get; set; }
	}

	public class Stats
	{
		public int CheckinsCount { get; set; }
		public int UsersCount { get; set; }
		public int TipCount { get; set; }
	}

	public class Specials
	{
		public int Count { get; set; }
		public object[] Items { get; set; }
	}

	public class Herenow
	{
		public int Count { get; set; }
		public string Summary { get; set; }
		public object[] Groups { get; set; }
	}

	public class Venuepage
	{
		public string Id { get; set; }
	}

	public class Menu
	{
		public string Type { get; set; }
		public string Label { get; set; }
		public string Anchor { get; set; }
		public string Url { get; set; }
		public string MobileUrl { get; set; }
	}

	public class Category
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string PluralName { get; set; }
		public string ShortName { get; set; }
		public Icon Icon { get; set; }
		public bool Primary { get; set; }
	}

	public class Icon
	{
		public string Prefix { get; set; }
		public string Suffix { get; set; }
	}


	public class Geocode
	{
		public string What { get; set; }
		public string Where { get; set; }
		public Feature Feature { get; set; }
	}

	public class Feature
	{
		public string CC { get; set; }
		public string Name { get; set; }
		public string DisplayName { get; set; }
		public string MatchedName { get; set; }
		public string HighlightedName { get; set; }
		public int WoeType { get; set; }
		public string Id { get; set; }
		public string LongId { get; set; }
		public Geometry Geometry { get; set; }
	}

	public class Geometry
	{
		public Center Center { get; set; }
		public Bounds Bounds { get; set; }
	}

	public class Center
	{
		public float Lat { get; set; }
		public float Lng { get; set; }
	}

	public class Bounds
	{
		public Ne Ne { get; set; }
		public Sw Sw { get; set; }
	}

	public class Ne
	{
		public float Lat { get; set; }
		public float Lng { get; set; }
	}

	public class Sw
	{
		public float Lat { get; set; }
		public float Lng { get; set; }
	}


}

