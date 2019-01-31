
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using BossMandadero.Adapters;
using Common.DBItems;
using Common.Utils;
using CoreLogic.ActivityCore;
using static Android.Widget.AdapterView;
using Android.Support.V4.Content;
using Android;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.Design.Widget;

namespace BossMandadero.Activities
{
    [Activity(Label = "PendingOrdersActivity", Theme = "@style/AppDrawerTheme")]
    public class PendingOrdersActivity : AppCompatActivity, IOnMapReadyCallback
    {
        
        private Drawer drawer;
        private MapInvoker map;
        private ListView ordersListView;
        private PendingOrdersCore core;


        //Location _currentLocation;
        //LocationManager _locationManager;

        //string _locationProvider;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PendingOrdersLayout);

            core = new PendingOrdersCore(this);
            map = new MapInvoker(this, MapType.Dialog);
            drawer = new Drawer(this);

            SetResources();
        }

        private void SetResources()
        {
            ordersListView = FindViewById<ListView>(Resource.Id.OrdersListView);
            PendingOrders();
        }

        private async void PendingOrders()
        {
            Dialogs.CreateProgressDialog(this, Resource.Style.AlertDialogDefault);
            List<Manboss_mandado> orders = await core.PendingOrders();

            PendingOrderAdapter adapter = new PendingOrderAdapter(this, orders, map);
            ordersListView.Adapter = adapter;
            Dialogs.DismissProgressDialog();
        }
        public void RequestLocationPermission()
        {
            var requiredPermissions = new String[] { Manifest.Permission.AccessFineLocation };
            if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.AccessFineLocation))
            {
                Snackbar.Make(FindViewById(Android.Resource.Id.Content),
                               Resource.String.pending_locationrationale,
                               Snackbar.LengthIndefinite)
                        .SetAction(Resource.String.ok,
                                   new Action<View>(delegate (View obj) {
                                       ActivityCompat.RequestPermissions(this, requiredPermissions, 1);
                                   }
                        )
                ).Show();
            }
            else
            {
                ActivityCompat.RequestPermissions(this, requiredPermissions, 1);
            }
        }
        public async void StartOrder(int OrderID)
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == (int)Permission.Granted)
            {
                await core.StartOrder(OrderID);
                Intent intent = new Intent(this, typeof(ActiveOrderActivity));
                this.StartActivity(intent);
            }
            else
            {
                RequestLocationPermission();
            }
        }



        public override void OnBackPressed()
        {
            if(map.Displayed)
            {
                map.DismissMap();
            }
            else
            {
                int style = Resource.Style.AlertDialogDefault;
                Dialogs.ExitDialog(this, style);
            }

        }

        //MAP 

        public void SetMap(int OrderID)
        {
            core.OrderID = OrderID;
            map.StartMap();
            map.MapFrag = MapFragment.NewInstance();
            map.MapFrag = FragmentManager.FindFragmentById(Resource.Id.map) as MapFragment;
            if (map.MapFrag == null)
            {
                GoogleMapOptions mapOptions = new GoogleMapOptions()
                    .InvokeMapType(GoogleMap.MapTypeSatellite)
                    .InvokeZoomControlsEnabled(false)
                    .InvokeCompassEnabled(true);

                Android.App.FragmentTransaction fragTx = FragmentManager.BeginTransaction();
                map.MapFrag = MapFragment.NewInstance(mapOptions);
                fragTx.Add(Resource.Id.map, map.MapFrag, "map");
                fragTx.Commit();
            }
            map.MapFrag.GetMapAsync(this);
        }
        public async void OnMapReady(GoogleMap googleMap)
        {
            map.Map = googleMap;
            map.Route = await core.Route(0);
            map.MapReady();
        }




       
    }
}
