using System;

namespace Common.MapItems
{
    public static class MapConstants
    {
        public static string strGoogleServerKey = "AIzaSyAX0RG5aeJD1B6RTeYszWYSnQQPNhgOnFc";
        public static string strGoogleServerDirKey = "AIzaSyAX0RG5aeJD1B6RTeYszWYSnQQPNhgOnFc";
        public static string strGoogleDirectionUrl = "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&key=" + strGoogleServerDirKey + "";
        public static string strGeoCodingUrl = "https://maps.googleapis.com/maps/api/geocode/json?{0}&key=" + strGoogleServerKey + "";

        public static string strException = "Exception";
        public static string strTextSource = "Source";
        public static string strTextDestination = "Destination";

        public static string strNoInternet = "No online connection. Please review your internet connection";
        public static string strPleaseWait = "Please wait...";
        public static string strUnableToConnect = "Unable to connect server!,Please try after sometime";
    }
}