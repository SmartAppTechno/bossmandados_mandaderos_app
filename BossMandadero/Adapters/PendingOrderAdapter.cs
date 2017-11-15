using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using BossMandadero.Activities;
using Common.DBItems;
using Common.Utils;
using static Android.Widget.AdapterView;

namespace BossMandadero.Adapters
{
    public class PendingOrderAdapter : BaseAdapter
    {
        private List<Manboss_mandado> pendingOrders;
        private Activity activity;
        private MapInvoker map;

        public PendingOrderAdapter(Activity activity, List<Manboss_mandado> pendingOrders, MapInvoker map)
        {
            this.activity = activity;
            this.pendingOrders = pendingOrders;
            this.map = map;
        }

        public override int Count
        {
            get { return pendingOrders.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return pendingOrders[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? activity.LayoutInflater.Inflate(
                Resource.Layout.PendingOrderItem, parent, false);


            TextView tv_Referencia = view.FindViewById<TextView>(Resource.Id.tv_Referencia);
            Button btn_Posicion = view.FindViewById<Button>(Resource.Id.btn_Posicion);
            btn_Posicion.Tag = position;


            tv_Referencia.Text = pendingOrders[position].Id.ToString();
            btn_Posicion.Click += ShowMap;

            return view;
        }

        private void ShowMap(object sender, EventArgs ea)
        {
            int DelPos = (int)((Button)sender).Tag;
            ((PendingOrdersActivity)activity).SetMap();
        }
    }
}
