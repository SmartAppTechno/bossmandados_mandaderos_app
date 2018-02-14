using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
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

        public override int ViewTypeCount
        {
            get
            {
                if (Count > 0) return Count;
                else return 1;
            }
        }

        public override int GetItemViewType(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return pendingOrders[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                View view = convertView ?? activity.LayoutInflater.Inflate(
                    Resource.Layout.PendingOrderItem, parent, false);


                TextView tv_Referencia = view.FindViewById<TextView>(Resource.Id.tv_Referencia);
                Button btn_Posicion = view.FindViewById<Button>(Resource.Id.btn_Posicion);
                btn_Posicion.TransformationMethod = null;
                btn_Posicion.SetAllCaps(false);
                ProgressBar bar = view.FindViewById<ProgressBar>(Resource.Id.progressBar);
                btn_Posicion.Tag = position;


                tv_Referencia.Text += pendingOrders[position].Id.ToString();

                if (position == 0)
                {
                    if (pendingOrders[0].Estado == 2)
                    {
                        btn_Posicion.Text = "Iniciar";
                        btn_Posicion.Click += StartOrder;
                        bar.Visibility = ViewStates.Gone;
                    }
                    else
                    {
                        btn_Posicion.Text = "Detalles";
                        btn_Posicion.Click += GoToOrder;
                        bar.Visibility = ViewStates.Visible;
                    }
                }
                else
                {
                    btn_Posicion.Click += ShowMap;
                    bar.Visibility = ViewStates.Gone;
                }


                return view;
            }else{
                return convertView;
            }
        }

        private void ShowMap(object sender, EventArgs ea)
        {
            int DelPos = (int)((Button)sender).Tag;
            int OrderID = pendingOrders[DelPos].Id;

            ((PendingOrdersActivity)activity).SetMap(OrderID);
        }
        private void StartOrder(object sender, EventArgs ea)
        {
            int pos = 0;
            int OrderID = pendingOrders[pos].Id;

            ((PendingOrdersActivity)activity).StartOrder(OrderID);
        }
        private void GoToOrder(object sender, EventArgs ea)
        {
            Intent intent = new Intent(activity, typeof(ActiveOrderActivity));
            activity.StartActivity(intent);
        }
    }
}
