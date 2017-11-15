
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Common.DBItems;
using CoreLogic.ActivityCore;

namespace BossMandadero.Activities
{
    [Activity(Label = "ActiveOrderActivity", Theme = "@style/AppDrawerTheme")]
    public class ActiveOrderActivity : AppCompatActivity, IOnMapReadyCallback
    {
        private Drawer drawer;
        private MapInvoker map;
        private ActiveOrderCore core;

        private TextView tv_Order;
        private TextView tv_Reference;
        private ListView lv_Pending;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActiveOrderLayout);

            core = new ActiveOrderCore(this);
            map = new MapInvoker(this, MapType.Embedded);
            drawer = new Drawer(this);


            SetResources();
        }

        private async void SetResources()
        {

            tv_Order = FindViewById<TextView>(Resource.Id.tv_Order);
            tv_Reference = FindViewById<TextView>(Resource.Id.tv_Reference);
            lv_Pending = FindViewById<ListView>(Resource.Id.lv_pending);

            Manboss_mandado mandado = await core.ActiveOrder();

            if (mandado != null)
            {
                tv_Order.Text += mandado.Id;
                tv_Reference.Text += mandado.Id;

                SetMap();
            }

        }
        public override void OnBackPressed()
        {
            Finish();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }

        public void SetMap()
        {
            map.StartMap();
            map.MapFrag = MapFragment.NewInstance();
            map.MapFrag = FragmentManager.FindFragmentById(Resource.Id.map) as MapFragment;
            if (map.MapFrag == null)
            {
                GoogleMapOptions mapOptions = new GoogleMapOptions()
                    .InvokeMapType(GoogleMap.MapTypeSatellite)
                    .InvokeZoomControlsEnabled(false)
                    .InvokeCompassEnabled(true);

                FragmentTransaction fragTx = FragmentManager.BeginTransaction();
                map.MapFrag = MapFragment.NewInstance(mapOptions);
                fragTx.Add(Resource.Id.map, map.MapFrag, "map");
                fragTx.Commit();
            }
            map.MapFrag.GetMapAsync(this);
        }
        public async void OnMapReady(GoogleMap googleMap)
        {
            map.Map = googleMap;
            map.Route = await core.Route();
            map.MapReady();
        }
    }
}
