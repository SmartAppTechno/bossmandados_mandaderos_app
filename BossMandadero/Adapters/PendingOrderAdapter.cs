using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Common.DBItems;
using Common.Utils;

namespace BossMandadero.Adapters
{
    public class PendingOrderAdapter : BaseAdapter
    {
        List<Manboss_mandado> pendingOrders;
        Activity activity;

        public PendingOrderAdapter(Activity activity, List<Manboss_mandado> pendingOrders)
        {
            this.activity = activity;
            this.pendingOrders = pendingOrders;
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

            tv_Referencia.Text = pendingOrders[position].Id.ToString();
            btn_Posicion.Click += Position;


            return view;
        }

        private void Position(object sender, EventArgs ea)
        {
            string message = "Click en Posicion";
            Dialogs.CreateAndShowDialog(message,"DEBUG",activity,Resource.Style.AlertDialogDefault);
        }
    }
}
