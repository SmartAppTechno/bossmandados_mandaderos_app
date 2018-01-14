using System;
using System.Collections.Generic;
using Android.App;
using Android.Gms.Maps;
using Android.Views;
using Android.Widget;
using BossMandadero.Activities;
using BossMandadero.Adapters;
using Common;
using Common.DBItems;
using CoreLogic.ActivityCore;

namespace BossMandadero
{
    public class OrderInvoker
    {
        private Activity mAct;
        public ActiveOrderCore core;
        public MapInvoker map;
        public Dialog mDialog;

        public bool Displayed { get; set; }

        private TextView txt_Name, txt_Direction, txt_City, txt_Task, txt_Detail;
        private TextView txt_TipoPago;
        private Button btn_Map, btn_List;
        private ImageView img_Chat;
        private View mapView;
        private View listView;
        private ListView routeListView;

        private Manboss_mandado order;
        private View view;

        public OrderInvoker(Activity activity)
        {
            mAct = activity;
            core = new ActiveOrderCore(mAct);
            map = new MapInvoker(mAct, MapType.Embedded);
            Display();
            Hide();
        }
        public void Display()
        {
            if (mDialog != null)
            {
                mDialog.Show();
            }
            else
            {
                mDialog = new Dialog(mAct, Resource.Style.AlertDialogWhite);
                LayoutInflater inflater = mAct.LayoutInflater;
                view = inflater.Inflate(Resource.Layout.ActiveOrderLayout, null);
                mDialog.SetContentView(view);
                mDialog.Window.SetSoftInputMode(SoftInput.StateHidden);
                mDialog.Show();
            }
            Displayed = true;
        }
        public void Hide()
        {
            mDialog.Hide();
            Displayed = false;
        }
        public void Dismiss()
        {
            mDialog.Dismiss();
            Displayed = true;
        }
        public void Display(Manboss_mandado order)
        {
            this.order = order;
            Display();
            SetResources();
        }
        private async void SetResources()
        {
            txt_Name = view.FindViewById<TextView>(Resource.Id.txt_Name);
            txt_Direction = view.FindViewById<TextView>(Resource.Id.txt_Direction);
            txt_City = view.FindViewById<TextView>(Resource.Id.txt_City);
            txt_Task = view.FindViewById<TextView>(Resource.Id.txt_Task);
            txt_Detail = view.FindViewById<TextView>(Resource.Id.txt_Detail);
            txt_TipoPago = view.FindViewById<TextView>(Resource.Id.txt_TipoPago);

            mapView = view.FindViewById<View>(Resource.Id.layoutMap);
            listView = view.FindViewById<View>(Resource.Id.layoutList);

            btn_Map = view.FindViewById<Button>(Resource.Id.btn_Map);
            btn_List = view.FindViewById<Button>(Resource.Id.btn_List);

            img_Chat = view.FindViewById<ImageView>(Resource.Id.img_Chat);
            img_Chat.Visibility = ViewStates.Gone;

            routeListView = view.FindViewById<ListView>(Resource.Id.TaskList);

            listView.Visibility = ViewStates.Gone;
            mapView.Visibility = ViewStates.Visible;

            btn_Map.Click += TabMap;
            btn_List.Click += TabList;

            Manboss_cliente client = await core.GetClient(order);
            txt_Name.Text = client.Nombre;
            txt_City.Text = map.GetCity(client.Latitud, client.Longitud);

            if (order.Tipo_pago == 0)
            {
                txt_TipoPago.Text = "Efectivo";
            }
            else
            {
                txt_TipoPago.Text = "Tarjeta";
            }

            ((ComissionsActivity)mAct).SetMap();

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

        public void RouteReady()
        {
            RouteAdapter adapter = new RouteAdapter(mAct, map.Route);
            routeListView.Adapter = adapter;

            if (map.Route.Count > 0)
            {
                int task = map.Route[0].Servicio - 1;
                txt_Task.Text = Services.Service[task];
                txt_Direction.Text = map.Route[0].Calle + " " + map.Route[0].Numero;
                txt_Detail.Text = map.Route[0].Comentarios;
            }
        }

    }
}
