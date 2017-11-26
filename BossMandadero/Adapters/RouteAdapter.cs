using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using BossMandadero.Activities;
using Common;
using Common.DBItems;
using Common.Utils;

namespace BossMandadero.Adapters
{
    public class RouteAdapter : BaseAdapter
    {
        private List<Manboss_mandados_ruta> route;
        private Activity activity;

        public RouteAdapter(Activity activity, List<Manboss_mandados_ruta> route)
        {
            this.activity = activity;
            this.route = route;
        }

        public override int Count
        {
            get { return route.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return route[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? activity.LayoutInflater.Inflate(
                Resource.Layout.RouteItem, parent, false);

            view.Tag = position;
            view.Click += TaskClick;

            TextView txt_Order = view.FindViewById<TextView>(Resource.Id.txt_Order);
            TextView txt_Direction = view.FindViewById<TextView>(Resource.Id.txt_Direction);
            TextView txt_Task = view.FindViewById<TextView>(Resource.Id.txt_Task);

            txt_Order.Text += " " + (position + 1);
            txt_Direction.Text = route[position].Calle + " " + route[position].Numero;

            int task = route[position].Servicio - 1;
            txt_Task.Text = Services.Service[task];


            return view;
        }
        private void TaskClick(object sender, EventArgs ea)
        {
            int position = (int)((View)sender).Tag;
            ((ActiveOrderActivity)activity).GoToMap(position);
        }
    }
}
