using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Common.MapItems;
using Android.OS;
using Android.Util;
using Common.DBItems;
using Java.IO;
using Java.Lang;
using System.Threading.Tasks;
using Android.Locations;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace BossMandadero
{
    public class Route
    {
        private WebClient webclient;

        private GoogleMap map;
        private MapInvoker invoker;
        private Activity act;

        private LatLng latLngSource;
        private LatLng latLngDestination;

        private string source;
        private string destination;

        public bool Complete { get; set; }


        public Route(MapInvoker invoker, LatLng position)
        {
            Complete = false;
            this.invoker = invoker;
            this.map = invoker.Map;
            this.act = invoker.mAct;
            if(position!=null)
                StartDrawing(position);

        }

        public void StartDrawing(LatLng position)
        {
            
            Complete = true;
            int n = invoker.Route.Count - 1;
            latLngSource = position;
            latLngDestination = GetLatLng(invoker.Route[0]);
            FnProcessOnMap();
            for (int i = 0; i < n;i++)
            {
                latLngSource = GetLatLng(invoker.Route[i]);
                latLngDestination = GetLatLng(invoker.Route[i+1]);
                SetStrings();
                FnProcessOnMap();
            }

        }

        private LatLng GetLatLng(Manboss_mandados_ruta r)
        {
            LatLng aux = new LatLng(r.Latitud, r.Longitud);
            return aux;
        }
        private void SetStrings()
        {
            source = latLngSource.Latitude.ToString() + "," + latLngSource.Longitude.ToString();
            destination = latLngDestination.Latitude.ToString() + "," + latLngDestination.Longitude.ToString();
        }

        private void FnProcessOnMap()
        {

            if (latLngSource != null && latLngDestination != null)
            {
                SetStrings();
                FnDrawPath();
            }
        }

        private async void FnDrawPath()
        {
            string strFullDirectionURL = string.Format(MapConstants.strGoogleDirectionUrl, source, destination);
            string strJSONDirectionResponse = await FnHttpRequest(strFullDirectionURL);
            if (strJSONDirectionResponse != MapConstants.strException)
            {
                FnSetDirectionQuery(strJSONDirectionResponse);
            }
            else
            {
            }

        }
        private async Task<string> FnHttpRequest(string strUri)
        {
            webclient = new WebClient();
            string strResultData;
            try
            {
                strResultData = await webclient.DownloadStringTaskAsync(new Uri(strUri));
            }
            catch
            {
                strResultData = MapConstants.strException;
            }
            finally
            {
                if (webclient != null)
                {
                    webclient.Dispose();
                    webclient = null;
                }
            }

            return strResultData;
        }


        private void FnSetDirectionQuery(string strJSONDirectionResponse)
        {
            var objRoutes = JsonConvert.DeserializeObject<GoogleDirectionClass>(strJSONDirectionResponse);
            //objRoutes.routes.Count  --may be more then one 
            if (objRoutes.routes.Count > 0)
            {
                string encodedPoints = objRoutes.routes[0].overview_polyline.points;

                var lstDecodedPoints = FnDecodePolylinePoints(encodedPoints);
                //convert list of location point to array of latlng type
                var latLngPoints = new LatLng[lstDecodedPoints.Count];
                int index = 0;
                foreach (Common.MapItems.Location loc in lstDecodedPoints)
                {
                    latLngPoints[index++] = new LatLng(loc.lat, loc.lng);
                }

                var polylineoption = new PolylineOptions();
                polylineoption.InvokeColor(new Android.Graphics.Color(99, 189, 181));
                polylineoption.Geodesic(true);
                polylineoption.Add(latLngPoints);
                act.RunOnUiThread(() =>
                map.AddPolyline(polylineoption));
            }
        }

        List<Common.MapItems.Location> FnDecodePolylinePoints(string encodedPoints)
        {
            if (string.IsNullOrEmpty(encodedPoints))
                return null;
            var poly = new List<Common.MapItems.Location>();
            char[] polylinechars = encodedPoints.ToCharArray();
            int index = 0;

            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;

            try
            {
                while (index < polylinechars.Length)
                {
                    // calculate next latitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length)
                        break;

                    currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                    //calculate next longitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length && next5bits >= 32)
                        break;

                    currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                    Common.MapItems.Location p = new Common.MapItems.Location()
                    {
                        lat = Convert.ToDouble(currentLat) / 100000.0,
                        lng = Convert.ToDouble(currentLng) / 100000.0
                    };
                    poly.Add(p);
                }
            }
            catch
            {
                //act.RunOnUiThread(() =>
                //  Toast.MakeText(this, MapConstants.strPleaseWait, ToastLength.Short).Show());
            }
            return poly;
        }



        string FnHttpRequestOnMainThread(string strUri)
        {
            webclient = new WebClient();
            string strResultData;
            try
            {
                strResultData = webclient.DownloadString(new Uri(strUri));
            }
            catch (System.Exception e)
            {
                strResultData = MapConstants.strException;
            }
            finally
            {
                if (webclient != null)
                {
                    webclient.Dispose();
                    webclient = null;
                }
            }

            return strResultData;
        }
    }
}