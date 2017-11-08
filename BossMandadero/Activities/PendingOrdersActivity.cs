
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
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

namespace BossMandadero.Activities
{
    [Activity(Label = "PendingOrdersActivity", Theme = "@style/AppDrawerTheme")]
    public class PendingOrdersActivity : AppCompatActivity
    {
        
        private Drawer drawer;
        private ListView ordersListView;
        private PendingOrdersCore core;
        private List<Manboss_mandado> orders;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PendingOrdersLayout);

            core = new PendingOrdersCore(this);

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
            orders = await core.PendingOrders();

            PendingOrderAdapter adapter = new PendingOrderAdapter(this, orders);
            ordersListView.Adapter = adapter;
        }



        public override void OnBackPressed()
        {
            Finish();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}
