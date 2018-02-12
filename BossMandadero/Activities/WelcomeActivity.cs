
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CoreLogic.ActivityCore;
using Android.Support.V7.App;
using Common.Utils;
using CoreLogic;

namespace BossMandadero.Activities
{
    [Activity(Label = "WelcomeActivity", Theme = "@style/AppDrawerTheme", NoHistory = true)]
    public class WelcomeActivity : AppCompatActivity
    {
        private WelcomeCore core;

        private EditText txt_Efectivo;

        private Button btn_Continue;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WelcomeLayout);

            core = new WelcomeCore(this);

            SetResources();
        }

        private void SetResources()
        {
            Dialogs.CreateProgressDialog(this, Resource.Style.AlertDialogDefault);
            //Get reference to the needed resources
            txt_Efectivo = FindViewById<EditText>(Resource.Id.txt_Efectivo);
            btn_Continue = FindViewById<Button>(Resource.Id.btn_Continue);

            //Set the button methods
            btn_Continue.Click += Continue;

            WaitEfectivo();
        }

        private async void WaitEfectivo(){
            double efectivo = await core.CheckIntegrity();
            string cash = efectivo.ToString();
            txt_Efectivo.Text = cash;
            Dialogs.DismissProgressDialog();
        }

        private async void Continue(object sender, EventArgs ea)
        {
            int style = Resource.Style.AlertDialogDefault;
            Dialogs.CreateProgressDialog(this, style);

            double efectivo = Convert.ToDouble(txt_Efectivo.Text);

            bool validation = await core.StartDay(efectivo);

            if (validation)
            {
                Intent nextActivity = new Intent(this, typeof(ProfileActivity));
                StartActivity(nextActivity);
            }
            else
            {
                string message = "Error de conexión";
                string title = "Error";

                Dialogs.CreateAndShowDialog(message, title, this, style);
            }

            Dialogs.DismissProgressDialog();

        }

        public override void OnBackPressed()
        {
            int style = Resource.Style.AlertDialogDefault;
            Dialogs.ExitDialog(this, style);
        }
    }
}
