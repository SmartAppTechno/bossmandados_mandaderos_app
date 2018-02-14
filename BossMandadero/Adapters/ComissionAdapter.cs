using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using BossMandadero.Activities;
using Common.DBItems;

namespace BossMandadero.Adapters
{
    public class ComissionAdapter : BaseAdapter
    {
        private List<Manboss_comision> comissions;
        private Activity activity;


        public ComissionAdapter(Activity activity, List<Manboss_comision> comissions)
        {
            this.activity = activity;
            this.comissions = comissions;
        }

        public override int Count
        {
            get { return comissions.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return comissions[position].Id;
        }

        public override int ViewTypeCount
        {
            get { return Count; }
        }

        public override int GetItemViewType(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            
            View view;
            if (convertView == null)
            {
                view = convertView ?? activity.LayoutInflater.Inflate(
                    Resource.Layout.Comission_item, parent, false);
                TextView txt_Mandado = view.FindViewById<TextView>(Resource.Id.txt_Mandado);
                TextView txt_Comision = view.FindViewById<TextView>(Resource.Id.txt_Comision);
                txt_Mandado.Text += " " + comissions[position].Mandado;

                string comision = string.Format("{0:0.00}", comissions[position].Comision);

                txt_Comision.Text += " " + comision;
                view.Tag = position;
                view.Click += ComissionClick;
                return view;
            }
            else{
                //view = convertView;
                return convertView;
            }







        }

        private void ComissionClick(object sender, EventArgs ea)
        {
            int position = (int)((View)sender).Tag;
            int id = comissions[position].Mandado;
            ((ComissionsActivity)activity).GetOrder(id);
        }
    }
}
