
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using BossMandadero.Adapters;
using Common;
using Common.DBItems;
using Common.Utils;
using CoreLogic;
using CoreLogic.ActivityCore;
using static Android.App.ActionBar;
using static Android.Gms.Maps.GoogleMap;

namespace BossMandadero.Activities
{
    [Activity(Label = "ActiveOrderActivity", Theme = "@style/AppDrawerTheme", NoHistory = true)]
    public class ActiveOrderActivity : AppCompatActivity, IOnMapReadyCallback, IOnMarkerClickListener, ILocationListener
    {
        private Drawer drawer;
        private MapInvoker map;
        private ChatInvoker chat;
        private ActiveOrderCore core;

        private TextView txt_Name, txt_Direction, txt_City, txt_Task, txt_Detail;
        private Button btn_Map, btn_List;
        private ImageView img_Chat;
        private TextView txt_TipoPago;

        private View mapView;
        private View listView;

        private ListView routeListView;

        private Manboss_mandados_ruta r_actual;
        private Dialog mDialog;
        Location _currentLocation;
        LocationManager _locationManager;

        string _locationProvider;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActiveOrderLayout);

            core = new ActiveOrderCore(this);
            map = new MapInvoker(this, MapType.Embedded);

            drawer = new Drawer(this);

            InitializeLocationManager();
            SetResources();
        }
        private void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }
        }
        private async void SetResources()
        {
            Dialogs.CreateProgressDialog(this,Resource.Style.AlertDialogDefault);
            Window.SetSoftInputMode(SoftInput.StateHidden);
            txt_Name = FindViewById<TextView>(Resource.Id.txt_Name);
            txt_Direction = FindViewById<TextView>(Resource.Id.txt_Direction);
            txt_City = FindViewById<TextView>(Resource.Id.txt_City);
            txt_Task = FindViewById<TextView>(Resource.Id.txt_Task);
            txt_Detail = FindViewById<TextView>(Resource.Id.txt_Detail);
            txt_TipoPago = FindViewById<TextView>(Resource.Id.txt_TipoPago);

            mapView = FindViewById<View>(Resource.Id.layoutMap);
            listView = FindViewById<View>(Resource.Id.layoutList);

            btn_Map = FindViewById<Button>(Resource.Id.btn_Map);
            btn_List = FindViewById<Button>(Resource.Id.btn_List);

            img_Chat = FindViewById<ImageView>(Resource.Id.img_Chat);

            routeListView = FindViewById<ListView>(Resource.Id.TaskList);

            listView.Visibility = ViewStates.Gone;
            mapView.Visibility = ViewStates.Visible;

            btn_Map.Click += TabMap;
            btn_List.Click += TabList;
            img_Chat.Click += ShowChat;

            Manboss_mandado mandado = await core.ActiveOrder();

            if(mandado != null)
            {
                Manboss_cliente client = core.Client;
                chat = new ChatInvoker(this, mandado.Id);
                txt_Name.Text = client.Nombre;
                txt_City.Text = map.GetCity(client.Latitud, client.Longitud);

                if(mandado.Tipo_pago == 0)
                {
                    txt_TipoPago.Text = "Efectivo";
                }else
                {
                    txt_TipoPago.Text = "Tarjeta";
                }

                SetMap();
            }
            Dialogs.DismissProgressDialog();

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
        public void ShowChat(object sender, EventArgs ea)
        {
            chat.Display();
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
            map.Route = await core.Route(0);

            RouteAdapter adapter = new RouteAdapter(this, map.Route);
            routeListView.Adapter = adapter;

            if(map.Route.Count > 0 )
            {
                SetDetallesRuta(map.Route[0]);
            }
            map.Map.SetOnMarkerClickListener(this);
            map.MapReady();
        }
        public void GoToMap(int position)
        {
            Manboss_mandados_ruta r = map.GoTo(position);
            SetDetallesRuta(r);
            TabMap(null, null);
        }

        public void SetDetallesRuta(Manboss_mandados_ruta r)
        {
            int task = r.Servicio - 1;
            txt_Task.Text = Common.Services.Service[task];
            txt_Direction.Text = r.Calle + " " + r.Numero;
            txt_Detail.Text = r.Comentarios;
        }

        public bool OnMarkerClick(Marker marker)
        {
            string msg = string.Empty;
            r_actual = map.MarkerClick(marker);
            if (r_actual != null)
            {

                Android.App.AlertDialog.Builder builder = 
                    Dialogs.YesNoDialog(string.Empty,string.Empty,this,Resource.Style.AlertDialogDefault);
                mDialog = builder.Show();
                LayoutInflater inflater = this.LayoutInflater;
                View view = inflater.Inflate(Resource.Layout.CompletePointLayout, null);
                mDialog.SetContentView(view);
                mDialog.Window.SetSoftInputMode(SoftInput.StateHidden);
                Window window = mDialog.Window;
                window.SetLayout(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                mDialog.Show();


                TextView txt_Comment = view.FindViewById<TextView>(Resource.Id.txt_Comment);
                TextView txt_Comments = view.FindViewById<TextView>(Resource.Id.txt_Comments);
                TextView txt_Complete = view.FindViewById<TextView>(Resource.Id.txt_Complete);
                Button btn_ok = view.FindViewById<Button>(Resource.Id.btn_ok);

                string aux = map.GetComentario(marker);
                if(aux.Length>0)
                {
                    txt_Comments.Text = aux;
                }
                else
                {
                    txt_Comment.Visibility = ViewStates.Gone;
                    txt_Comments.Visibility = ViewStates.Gone;
                }

                if(map.Route.Count==1)
                {
                    txt_Complete.Text = this.Resources.GetString(Resource.String.active_completeOrder);

                }else
                {
                    txt_Complete.Text = this.Resources.GetString(Resource.String.active_completePoint);
                }

                btn_ok.Click += RemoveMarker;



            }
            return true;
        }
        public async void RemoveMarker(object sender, EventArgs e)
        {
            Dialogs.CreateProgressDialog(this,Resource.Style.AlertDialogDefault);
            mDialog.Dismiss();
            if(r_actual!=null)
            {
                double total = await core.CompleteTask(r_actual);
                r_actual = null;
                if (core.route.Count == 1)
                {
                    string result = string.Format("{0:0.00}", total);
                    Android.App.AlertDialog.Builder builder = Dialogs.YesNoDialog(
                        "Total","El total del mandado fué: \n\n  $ " + result ,this,Resource.Style.AlertDialogDefault);
                    builder.SetPositiveButton("OK", EndOrder);
                    Dialog d = builder.Show();
                    
                }
                else
                {
                    OnMapReady(map.Map);
                }
            }
            Dialogs.DismissProgressDialog();
        }

        public async void EndOrder(object sender, DialogClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(PendingOrdersActivity));
            this.StartActivity(intent);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }
        protected override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }

        public void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            if (_currentLocation != null)
            {
                map.Position = new LatLng(location.Latitude, location.Longitude);
                map.PositionChanged();
            }/*
            if(User.CanSetUbicacion)
            {
                User.UpdateLocation(location,this);
            }*/
        }

        public void OnProviderDisabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            //throw new NotImplementedException();
        }
    }
}
