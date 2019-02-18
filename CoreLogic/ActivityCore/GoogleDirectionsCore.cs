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
using Android.Gms.Maps;
using System.Threading.Tasks;
using Android.Gms.Maps.Model;
using DataAccess.ActivityData;

namespace CoreLogic.ActivityCore
{

    public class GoogleDirectionsCore
    {
        private GoogleDirectionsData data;
        private GoogleMap map;
        private Context context;

        public GoogleDirectionsCore(GoogleMap map, Context context)
        {
            this.map = map;
            this.context = context;
            data = new GoogleDirectionsData(context);
        }

        public async Task<Polyline> AddRoadPolyline(int MandadoID)
        {
            string serial = await data.GetPolyline(MandadoID);
            Java.Util.ArrayList points = new Java.Util.ArrayList(DecodePolyline(serial));

            PolylineOptions options = new PolylineOptions();
            options.AddAll(points);

            Polyline ans = map.AddPolyline(options);
            return ans;
        }

        public List<LatLng> DecodePolyline(string serial)
        {
            if (string.IsNullOrWhiteSpace(serial))
            {
                return null;
            }

            int index = 0;
            var polylineChars = serial.ToCharArray();
            var poly = new List<LatLng>();
            int currentLat = 0;
            int currentLng = 0;
            int next5Bits;

            while (index < polylineChars.Length)
            {
                // calculate next latitude
                int sum = 0;
                int shifter = 0;

                do
                {
                    next5Bits = polylineChars[index++] - 63;
                    sum |= (next5Bits & 31) << shifter;
                    shifter += 5;
                }
                while (next5Bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length)
                {
                    break;
                }

                currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                // calculate next longitude
                sum = 0;
                shifter = 0;

                do
                {
                    next5Bits = polylineChars[index++] - 63;
                    sum |= (next5Bits & 31) << shifter;
                    shifter += 5;
                }
                while (next5Bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length && next5Bits >= 32)
                {
                    break;
                }

                currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                var mLatLng = new LatLng(Convert.ToDouble(currentLat) / 100000.0, Convert.ToDouble(currentLng) / 100000.0);
                poly.Add(mLatLng);
            }

            return poly;
        }

    }
}