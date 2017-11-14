
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BossMandadero
{
    public class MapInvoker : Activity
    {
        private GoogleMap mMap;
        private MapFragment mFrag;
        private Activity mAct;
        private MapType mType;

        public MapInvoker(Activity activity, MapType type)
        {
            mAct = activity;
            mType = type;
            DisplayMap();
            SetUpMap();
        }
        public void OnMapReady(GoogleMap googleMap)
        {
            mMap = googleMap;
        }
        private void DisplayMap()
        {

        }
        /*
        public static void TestCreateProgressDialog(Activity activity, Context context, int layout, int style, int gif)
        {
            _progressDialog = new Dialog(activity, style);
            //AlertDialog.Builder builder = new AlertDialog.Builder(new ContextThemeWrapper(activity, style));
            LayoutInflater inflater = activity.LayoutInflater;
            View view = inflater.Inflate(layout, null);
            WebView iv = view.FindViewById<WebView>(gif);
            //iv.SetBackgroundColor(Color.Azure);
            iv.LoadUrl(string.Format("file:///android_asset/Loading.gif"));
            iv.SetBackgroundColor(new Color(0, 0, 0, 0));
            iv.SetLayerType(LayerType.Software, null);

            _progressDialog.SetContentView(view);

            //builder.SetView(inflater.Inflate(layout, null));

            //_progressDialog = builder.Create();

            _progressDialog.SetCanceledOnTouchOutside(false);
            _progressDialog.Show();
        }
        public static void TestDismissProgressDialog()
        {
            _progressDialog.Dismiss();
            _progressDialog = null;
        } 
        */
        private void SetUpMap()
        {
            /*
            mFrag = FragmentManager.FindFragmentById(Resource.Id.map) as MapFragment;
            if (mFrag == null)
            {
                GoogleMapOptions mapOptions = new GoogleMapOptions()
                    .InvokeMapType(GoogleMap.MapTypeSatellite)
                    .InvokeZoomControlsEnabled(false)
                    .InvokeCompassEnabled(true);

                FragmentTransaction fragTx = FragmentManager.BeginTransaction();
                mFrag = MapFragment.NewInstance(mapOptions);
                fragTx.Add(Resource.Id.map, mFrag, "map");
                fragTx.Commit();
            }
            mFrag.GetMapAsync(this);
            */
        }
    }

    public enum MapType
    {
        Route
    }
}
