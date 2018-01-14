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

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? activity.LayoutInflater.Inflate(
                Resource.Layout.Comission_item, parent, false);

            view.Tag = position;
            view.Click += ComissionClick;

            TextView txt_Mandado = view.FindViewById<TextView>(Resource.Id.txt_Mandado);
            TextView txt_Comision = view.FindViewById<TextView>(Resource.Id.txt_Comision);

            txt_Mandado.Text += " " + comissions[position].Mandado;
            txt_Comision.Text += " " + comissions[position].Comision;

            return view;
        }

        private void ComissionClick(object sender, EventArgs ea)
        {
            int position = (int)((View)sender).Tag;
            int id = comissions[position].Mandado;
            ((ComissionsActivity)activity).GetOrder(id);
        }
    }
}
