using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BossMandadero.Activities;
using Common.DBItems;
using Common.Utils;
using Java.Util;
using static Android.Gms.Maps.GoogleMap;

namespace BossMandadero
{
    public class MapInvoker
    {
        public GoogleMap Map { get; set; }
        public MapFragment MapFrag { get; set; }
        public Activity mAct;
        public LatLng Position { get; set; }
        public Route routeDrawer;
        private MapType mType;

        private Dialog mDialog;

        public List<Manboss_mandados_ruta> Route { get; set; }
        private List<Marker> markers;
        private Marker Repartidor;
        public bool Displayed { get; set; }


        public MapInvoker(Activity activity, MapType type)
        {
            mAct = activity;
            mType = type;
            Displayed = false;
            markers = new List<Marker>();
        }
        public bool StartMap()
        {
            if(mType == MapType.Dialog)
            {
                DisplayMap();
            }
            return true;
        }
        private void DisplayMap()
        {
            if (mDialog != null)
            {
                mDialog.Show();
            }
            else
            {
                mDialog = new Dialog(mAct, Resource.Style.AlertDialogDefault);
                LayoutInflater inflater = mAct.LayoutInflater;
                View view = inflater.Inflate(Resource.Layout.Map, null);
                mDialog.SetContentView(view);
                mDialog.Show();
                Displayed = true;
            }
        }
        public void DismissMap()
        {
            mDialog.Hide();
            Displayed = false;
        } 



        public void MapReady()
        {
            Map.UiSettings.ZoomControlsEnabled = true;
            Map.Clear();
            double lat = 0;
            double lng = 0;

            if(Route.Count() > 0)
            {
                lat = Route[0].Latitud;
                lng = Route[0].Longitud;
            }

            CameraPosition.Builder builder = new CameraPosition.Builder();
            builder.Target(new LatLng(lat, lng));
            builder.Zoom(13);
            CameraPosition cameraPosition = builder.Build();

            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            Map.MoveCamera(cameraUpdate);
            Map.MyLocationEnabled = true;

            foreach (Marker m in markers)
            {
                m.Remove();
            }
            markers.Clear();
            foreach(Manboss_mandados_ruta r in Route)
            {
                AddMarker(r.Latitud, r.Longitud);
            }

            routeDrawer = new Route(this, Position);
        }

        private void AddMarker(double lat, double lng)
        {
            MarkerOptions marker = new MarkerOptions();
            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.mandado));
            marker.SetPosition(new LatLng(lat,lng));
            Marker aux = Map.AddMarker(marker);
            markers.Add(aux); 
        }
        public Manboss_mandados_ruta GoTo(int position)
        {
            double lat = Route[position].Latitud;
            double lng = Route[position].Longitud;

            CameraPosition.Builder builder = new CameraPosition.Builder();
            builder.Target(new LatLng(lat, lng));
            builder.Zoom(13);
            CameraPosition cameraPosition = builder.Build();

            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            Map.MoveCamera(cameraUpdate);

            return Route[position];
        }
        public String GetCity(double? lat, double? lng){

            if (lat != null && lng != null)
            {
                Geocoder gcd = new Geocoder(mAct, Locale.Default);
                IList<Address> addresses = gcd.GetFromLocation((double)lat, (double)lng, 1);
                if (addresses.Count > 0)
                {
                    return addresses[0].Locality;
                }
            }

            return string.Empty;
        }



        public Manboss_mandados_ruta MarkerClick(Marker marker)
        {
            int n = 0;
            foreach(Marker m in markers)
            {
                if(m.Equals(marker))
                {
                    Manboss_mandados_ruta r = Route[n];
                    return r;
                }
                n++;
            }
            return null;
        }

        public string GetComentario(Marker marker)
        {
            int n = 0;
            foreach (Marker m in markers)
            {
                if (m.Equals(marker))
                {
                    Manboss_mandados_ruta r = Route[n];
                    return r.Comentarios;
                }
                n++;
            }
            return string.Empty;
        }
        public void PositionChanged()
        {
            if(Repartidor!=null)
                Repartidor.Remove();
            if (!routeDrawer.Complete)
            {
                routeDrawer.StartDrawing(Position);
            }
            MarkerOptions marker = new MarkerOptions();
            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.repartidor));
            marker.SetPosition(new LatLng(Position.Latitude, Position.Longitude));
            Repartidor = Map.AddMarker(marker);
        }
    }

    public enum MapType
    {
        Dialog, Embedded
    }
}
