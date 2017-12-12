using System;
using Android.App;
using Android.Views;

namespace BossMandadero
{
    public class ChatInvoker
    {
        private Activity mAct;
        private Dialog mDialog;
        public bool Displayed { get; set; }
        public ChatInvoker(Activity activity, int mandadoID)
        {
            mAct = activity;
            Displayed = false;
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
                View view = inflater.Inflate(Resource.Layout.ChatLayout, null);
                mDialog.SetContentView(view);
                mDialog.Show();
                Displayed = true;
            }
        }
    }
}
