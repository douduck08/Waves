using UnityEngine;
using System;

namespace TeamSignal.Utilities
{
	public partial class TSUtil
	{		
		/// <summary>
		/// Checks the internet.
		/// </summary>
		/// <returns><c>true</c>, if internet was checked, <c>false</c> otherwise.</returns>
		/// ReachableViaLocalAreaNetwork only Know WIFI connect , if WIFI no connect to Internet, it still return true
		static public bool CheckInternet()
		{
			switch(Application.internetReachability)
			{
			case NetworkReachability.NotReachable:
				Debug.Log( "Internet NotReachable");
				return false;
				
			case NetworkReachability.ReachableViaCarrierDataNetwork:
				Debug.Log( "Internet ReachableViaCarrierDataNetwork" );
				return true;
				
			case NetworkReachability.ReachableViaLocalAreaNetwork:
				Debug.Log( "Internet ReachableViaLocalAreaNetwork" );
				return true;
			}
			
			return false;
		}
		
		public static string ColorToHex(Color32 color)
		{
			string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
			return hex;
		}
		
		public static Color HexToColor(string hex)
		{
			try
			{
				hex = hex.Replace ("0x", "");	//in case the string is formatted 0xFFFFFF
				hex = hex.Replace ("#", "");	//in case the string is formatted #FFFFFF
				byte a = 255;	//assume fully visible unless specified in hex
				byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
				byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
				byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
				//Only use alpha if the string has enough characters
				if(hex.Length == 8)
				{
					a = byte.Parse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber);
				}
				
				return new Color32(r,g,b,a);
			}
			catch
			{
				Debug.LogError("HexToColor Error: " + hex);
				return Color.white;
			}
		}
	}
}
