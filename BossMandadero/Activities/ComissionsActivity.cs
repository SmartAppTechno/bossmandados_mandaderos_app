
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
using BossMandadero.Adapters;
using Common.DBItems;
using Common.Utils;
using CoreLogic.ActivityCore;

namespace BossMandadero.Activities
{
    [Activity(Label = "ComissionsActivity", Theme = "@style/AppDrawerTheme", NoHistory = true)]
    public class ComissionsActivity : AppCompatActivity, IOnMapReadyCallback
    {

        private ComissionsCore core;
        private Drawer drawer;
        private ListView list;
        private OrderInvoker orderInvoker;

        private Spinner day, month, year;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.ComissionsLayout);

            drawer = new Drawer(this);
            core = new ComissionsCore(this);
            orderInvoker = new OrderInvoker(this);
            SetResources();

        }
        private async void SetResources()
        {
            //Get reference to the needed resources
            list = FindViewById<ListView>(Resource.Id.ComissionsListView);

           
            SetSpinners();
            List<Manboss_comision> comissions = await core.GetComissions();
            ComissionAdapter adapter = new ComissionAdapter(this, comissions);
            list.Adapter = adapter;

        }
        private void SetSpinners()
        {
            day = FindViewById<Spinner>(Resource.Id.spinner_day);
            month = FindViewById<Spinner>(Resource.Id.spinner_month);
            year = FindViewById<Spinner>(Resource.Id.spinner_year);

            List<string> days = core.Days;
            List<string> months = core.Months;
            List<string> years = core.Years;

            for (int i = 1; i < 32; i++)
            {
                days.Add(i.ToString());
            }

            ArrayAdapter adapterDays = new ArrayAdapter(this,Android.Resource.Layout.SimpleListItem1, days);
            ArrayAdapter adapterMonths = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, months);
            ArrayAdapter adapterYears = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, years);

            day.Adapter = adapterDays;
            month.Adapter = adapterMonths;
            year.Adapter = adapterYears;

            day.ItemSelected += spinner_ItemSelected;
            month.ItemSelected += spinner_ItemSelected;
            year.ItemSelected += spinner_ItemSelected;


        }

        private async void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            int i_day = day.SelectedItemPosition;
            int i_month = month.SelectedItemPosition;
            int i_year = year.SelectedItemPosition;

            List<Manboss_comision> comissions = await core.Filter(i_year,i_month,i_day);
            ComissionAdapter adapter = new ComissionAdapter(this, comissions);
            list.Adapter = adapter;
        }

        public async void GetOrder(int id)
        {
            Manboss_mandado order = await core.GetMandado(id);
            orderInvoker.Display(order);
        }

        public override void OnBackPressed()
        {
            if (orderInvoker.mDialog.IsShowing)
            {
                orderInvoker.Hide();
            }
            else
            {
                int style = Resource.Style.AlertDialogWhite;
                Dialogs.ExitDialog(this, style);
            }
        }

        public void SetMap()
        {
            orderInvoker.map.StartMap();
            orderInvoker.map.MapFrag = MapFragment.NewInstance();
            orderInvoker.map.MapFrag = FragmentManager.FindFragmentById(Resource.Id.map) as MapFragment;
            if (orderInvoker.map.MapFrag == null)
            {
                GoogleMapOptions mapOptions = new GoogleMapOptions()
                    .InvokeMapType(GoogleMap.MapTypeSatellite)
                    .InvokeZoomControlsEnabled(false)
                    .InvokeCompassEnabled(true);

                FragmentTransaction fragTx = FragmentManager.BeginTransaction();
                orderInvoker.map.MapFrag = MapFragment.NewInstance(mapOptions);
                fragTx.Add(Resource.Id.map, orderInvoker.map.MapFrag, "map");
                fragTx.Commit();
            }
            orderInvoker.map.MapFrag.GetMapAsync(this);
        }
        public async void OnMapReady(GoogleMap googleMap)
        {
            orderInvoker.map.Map = googleMap;
            orderInvoker.map.Route = await orderInvoker.core.Route(1);
            orderInvoker.map.MapReady();

            orderInvoker.RouteReady();
        }
    }
}
