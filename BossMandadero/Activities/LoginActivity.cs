
using System;
using System.Collections.Generic;
using Android.Graphics;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Common.DBItems;
using Common.Utils;
using CoreLogic.ActivityCore;
using CoreLogic;
using Felipecsl.GifImageViewLibrary;
using System.IO;
using Android.Webkit;
using Android.Preferences;

namespace BossMandadero.Activities
{
    [Activity(Label = "LoginActivity", Theme = "@style/MainTheme")]
    public class LoginActivity : Activity
    {
        private LoginCore loginCore;

        private EditText txt_Email;
        private EditText txt_Password;

        private Button btn_Register;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.LoginLayout);

            loginCore = new LoginCore(this);
            SetResources();

        }
        private void SetResources()
        {
            //Get reference to the needed resources
            txt_Email = FindViewById<EditText>(Resource.Id.txt_Email);
            txt_Password = FindViewById<EditText>(Resource.Id.txt_Password);
            btn_Register = FindViewById<Button>(Resource.Id.btn_Register);

            //Set the button methods
            btn_Register.Click += Register;
        }
        private async void Register(object sender, EventArgs ea)
        {
            
            int layout = Resource.Layout.Loading;
            int style = Resource.Style.AlertDialogDefault;
            Dialogs.CreateProgressDialog(this, style);

            btn_Register.SetBackgroundColor(new Color(229, 85, 97));

            string email = txt_Email.Text;
            string password = txt_Password.Text;




            Manboss_usuario validation = await loginCore.Register(email, password);


            if (validation != null)
            {
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutInt("BossMandados_UserID", validation.Id);
                editor.Apply();

                Intent nextActivity = new Intent(this, typeof(WelcomeActivity));
                StartActivity(nextActivity);
            }
            else
            {
                string message = "Usuario o Contraseña incorrectos";
                string title = "Error al acceder";

                Dialogs.CreateAndShowDialog(message,title,this,style);
            }
            btn_Register.SetBackgroundColor(new Color(249, 00, 77));
            Dialogs.DismissProgressDialog();

        }


    }
}
