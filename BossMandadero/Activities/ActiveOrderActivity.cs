﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using BossMandadero.Adapters;
using Common;
using Common.DBItems;
using CoreLogic.ActivityCore;
using static Android.App.ActionBar;

namespace BossMandadero.Activities
{
    [Activity(Label = "ActiveOrderActivity", Theme = "@style/AppDrawerTheme")]
    public class ActiveOrderActivity : AppCompatActivity, IOnMapReadyCallback
    {
        private Drawer drawer;
        private MapInvoker map;
        private ActiveOrderCore core;

        private TextView txt_Name, txt_Direction, txt_City, txt_Task, txt_Detail;
        private Button btn_Map, btn_List;

        private View mapView;
        private View listView;

        private ListView routeListView;

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
            txt_Name = FindViewById<TextView>(Resource.Id.txt_Name);
            txt_Direction = FindViewById<TextView>(Resource.Id.txt_Direction);
            txt_City = FindViewById<TextView>(Resource.Id.txt_City);
            txt_Task = FindViewById<TextView>(Resource.Id.txt_Task);
            txt_Detail = FindViewById<TextView>(Resource.Id.txt_Detail);

            mapView = FindViewById<View>(Resource.Id.layoutMap);
            listView = FindViewById<View>(Resource.Id.layoutList);

            btn_Map = FindViewById<Button>(Resource.Id.btn_Map);
            btn_List = FindViewById<Button>(Resource.Id.btn_List);

            routeListView = FindViewById<ListView>(Resource.Id.TaskList);

            mapView.Visibility = ViewStates.Visible;
            mapView.Visibility = ViewStates.Invisible;

            btn_Map.Click += TabMap;
            btn_List.Click += TabList;

            Manboss_mandado mandado = await core.ActiveOrder();

            if(mandado != null)
            {
                Manboss_cliente client = core.Client;
                txt_Name.Text = client.Nombre;
                txt_City.Text = map.GetCity(client.Latitud, client.Longitud);
                SetMap();
            }

        }

        public void TabMap(object sender, EventArgs ea)
        {
            mapView.Visibility = ViewStates.Visible;
            listView.Visibility = ViewStates.Gone;
            btn_Map.SetBackgroundColor(Colors.TabActive);
            btn_List.SetBackgroundColor(Colors.TabInactive);
        }
        public void TabList(object sender, EventArgs ea)
        {
            mapView.Visibility = ViewStates.Gone;
            listView.Visibility = ViewStates.Visible;
            btn_Map.SetBackgroundColor(Colors.TabInactive);
            btn_List.SetBackgroundColor(Colors.TabActive);
        }

        public override void OnBackPressed()
        {
            Finish();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }


        //MaP
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

            RouteAdapter adapter = new RouteAdapter(this, map.Route);
            routeListView.Adapter = adapter;

            if(map.Route.Count > 0 )
            {
                txt_Task.Text = map.Route[0].Servicio.ToString();
                txt_Direction.Text = map.Route[0].Calle + " " + map.Route[0].Numero;
                txt_Detail.Text = map.Route[0].Comentarios;
            }

            map.MapReady();
        }
        public void GoToMap(int position)
        {
            map.GoTo(position);
            TabMap(null, null);
        }
    }
}
