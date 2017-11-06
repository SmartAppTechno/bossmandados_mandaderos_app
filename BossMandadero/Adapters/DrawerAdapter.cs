using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Common.Items;

namespace BossMandadero
{
    public class DrawerAdapter : ArrayAdapter<DrawerElement>
    {
        private System.Collections.Generic.List<DrawerElement> dataSet;
        Context mContext;

        private int lastPosition = -1;


        public DrawerAdapter(List<DrawerElement> data, Context context)
            :base(context, Resource.Layout.Drawer_item,data)
        {
            this.dataSet = data;
            this.mContext = context;

        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Activity activity = (Activity)mContext;
            var view = convertView;
            if (view == null)
            {
                view = activity.LayoutInflater.Inflate(Resource.Layout.Drawer_item, null);
            }
            var txt_drawer = view.FindViewById<TextView>(Resource.Id.drawer_txt);


            txt_drawer.Text = dataSet[position].Name;

            return view;
        }


    }
}
