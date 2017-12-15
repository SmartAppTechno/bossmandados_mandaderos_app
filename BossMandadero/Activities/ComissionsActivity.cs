
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
using CoreLogic.ActivityCore;

namespace BossMandadero.Activities
{
    [Activity(Label = "ComissionsActivity", Theme = "@style/AppDrawerTheme")]
    public class ComissionsActivity : AppCompatActivity
    {

        private ComissionsCore core;
        private Drawer drawer;
        private ListView list;

        private Spinner day, month, year;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.ComissionsLayout);

            drawer = new Drawer(this);
            core = new ComissionsCore(this);
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

            List<int> days = core.Days;
            List<int> months = core.Months;
            List<int> years = core.Years;

            for (int i = 0; i < 32; i++)
            {
                days.Add(i);
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
    }
}
