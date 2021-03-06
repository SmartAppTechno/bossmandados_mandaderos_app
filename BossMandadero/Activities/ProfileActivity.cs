﻿
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

using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using CoreLogic;
using CoreLogic.ActivityCore;
using Common.Utils;
using BossMandadero.Services;
using Android.Util;

namespace BossMandadero.Activities
{
    [Activity(Label = "ProfileActivity", Theme = "@style/AppDrawerTheme", NoHistory = true)]
    public class ProfileActivity : AppCompatActivity
    {

        private Drawer drawer;

        private TextView name;
        private TextView raiting;
        private TextView email;
        private TextView address;
        private Button logout;

        private ProfileCore core;
        public const string TAG = "Profile";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProfileLayout);

            drawer = new Drawer(this);
            core = new ProfileCore(this);
            SetResources();
            ServiceGPS.mContext = this;

            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    if (key != null)
                    {
                        var value = Intent.Extras.GetString(key);
                        Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                    }
                }
            }

            StartService(new Intent(this, typeof(ServiceGPS)));
            //StartService(new Intent(this, typeof(MyFirebaseIIDService)));
            //StartService(new Intent(this, typeof(MyFirebaseMessagingService)));
        }

        private void SetResources()
        {
            //Get reference to the needed resources
            name = FindViewById<TextView>(Resource.Id.txt_Name);
            raiting = FindViewById<TextView>(Resource.Id.txt_Raiting);
            email = FindViewById<TextView>(Resource.Id.txt_Email);
            logout = FindViewById<Button>(Resource.Id.btn_End);
            address = FindViewById<TextView>(Resource.Id.txt_Address);
            //Set the button methods
            name.Text = User.Usuario.Nombre;
            raiting.Text = User.Repartidor.Rating.ToString();
            email.Text = User.Usuario.Correo;
            address.Text = User.Repartidor.Direccion;

            logout.Click += Logout;
        }

        public override void OnBackPressed()
        {
            int style = Resource.Style.AlertDialogDefault;
            Dialogs.ExitDialog(this, style);
        }

        private async void Logout(object sender, EventArgs e)
        {
            Dialogs.CreateProgressDialog(this, Resource.Style.AlertDialogDefault);
            bool ans = await core.Logout();
            Dialogs.DismissProgressDialog();
            if(ans)
            {
                Intent nextActivity = new Intent(this, typeof(LoginActivity));
                this.StartActivity(nextActivity);
            }
        }

    }
}
