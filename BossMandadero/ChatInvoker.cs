using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using BossMandadero.Adapters;
using Common.DBItems;
using CoreLogic.ActivityCore;

namespace BossMandadero
{
    public class ChatInvoker
    {
        private Activity mAct;
        private Dialog mDialog;
        private ChatCore core;
        public bool Displayed { get; set; }

        private ListView chat;
        private Button send;
        private EditText message;

        private ChatAdapter adapter;
        private View view;

        public ChatInvoker(Activity activity, int mandadoID)
        {
            mAct = activity;
            Displayed = false;

            core = new ChatCore(activity, mandadoID);
            Display();
            Hide();
            SetResources(true);
        }
        public void Display()
        {
            if (mDialog != null)
            {
                mDialog.Show();
            }
            else
            {
                mDialog = new Dialog(mAct, Resource.Style.AlertDialogDefault);
                LayoutInflater inflater = mAct.LayoutInflater;
                view = inflater.Inflate(Resource.Layout.ChatLayout, null);
                mDialog.SetContentView(view);
                mDialog.Window.SetSoftInputMode(SoftInput.StateHidden);
                mDialog.Show();
                Displayed = true;
            }
        }
        public void Hide()
        {
            mDialog.Hide();
            Displayed = false;
        }
        private async void SetResources(bool setClick)
        {
            
            int id = await core.Chat();
            List<Manboss_chat_mensaje> messages = null;
            if (id != 0)
            {
                messages = await core.Conversation();
            }

            chat = view.FindViewById<ListView>(Resource.Id.chat);

            chat.Divider = null;
            chat.DividerHeight = 0;

            send = view.FindViewById<Button>(Resource.Id.btn_send);
            message = view.FindViewById<EditText>(Resource.Id.chat_mensaje);
            if(setClick)
                send.Click += SendMessage;
            send.Visibility = ViewStates.Visible;


            if(messages!=null)
            {
                adapter = new ChatAdapter(mAct, messages);
                chat.Adapter = adapter;

            }

        }

        private async void SendMessage(object sender, EventArgs e)
        {
            string text = message.Text;
            await core.Message(text);
            message.Text = string.Empty;
            SetResources(false);
        }


    }
}
