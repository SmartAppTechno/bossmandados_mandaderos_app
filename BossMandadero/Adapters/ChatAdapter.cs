using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Common.DBItems;

namespace BossMandadero.Adapters
{
    public class ChatAdapter : BaseAdapter
    {
        private List<Manboss_chat_mensaje> messages;
        private Activity activity;

        public ChatAdapter(Activity activity, List<Manboss_chat_mensaje> messages)
        {
            this.activity = activity;
            this.messages = messages;
        }

        public override int Count
        {
            get { return messages.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return messages[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? activity.LayoutInflater.Inflate(
                Resource.Layout.Chat_item, parent, false);

            view.Tag = position;

            TextView txt_Repartidor = view.FindViewById<TextView>(Resource.Id.txt_Repartidor);
            TextView txt_Cliente = view.FindViewById<TextView>(Resource.Id.txt_Cliente);
            TextView lbl_Repartidor = view.FindViewById<TextView>(Resource.Id.label_Repartidor);
            TextView lbl_Cliente = view.FindViewById<TextView>(Resource.Id.label_Cliente);

            if(messages[position].Rol == 2)
            {
                txt_Repartidor.Visibility = ViewStates.Visible;
                txt_Repartidor.Text = messages[position].Mensaje;
                lbl_Repartidor.Visibility = ViewStates.Visible;
                txt_Cliente.Visibility = ViewStates.Gone;
                lbl_Cliente.Visibility = ViewStates.Gone;
            }
            else
            {
                txt_Repartidor.Visibility = ViewStates.Gone;
                lbl_Repartidor.Visibility = ViewStates.Gone;

                txt_Cliente.Visibility = ViewStates.Visible;
                txt_Cliente.Text = messages[position].Mensaje;
                lbl_Cliente.Visibility = ViewStates.Visible;
            }


            return view;
        }
    }
}
