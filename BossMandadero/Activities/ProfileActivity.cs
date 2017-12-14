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

namespace BossMandadero.Activities
{
    [Activity(Label = "ProfileActivity", Theme = "@style/AppDrawerTheme")]
    public class ProfileActivity : AppCompatActivity
    {

        private Drawer drawer;

        private TextView name;
        private TextView raiting;
        private TextView email;
        private Button logout;

        private ProfileCore core;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProfileLayout);

            drawer = new Drawer(this);
            core = new ProfileCore(this);
            SetResources();
        }

        private void SetResources()
        {
            //Get reference to the needed resources
            name = FindViewById<TextView>(Resource.Id.txt_Name);
            raiting = FindViewById<TextView>(Resource.Id.txt_Raiting);
            email = FindViewById<TextView>(Resource.Id.txt_Email);
            logout = FindViewById<Button>(Resource.Id.btn_End);
            //Set the button methods
            name.Text = User.Usuario.Nombre;
            raiting.Text = User.Repartidor.Rating.ToString();
            email.Text = User.Usuario.Correo;

            logout.Click += Logout;
        }

        public override void OnBackPressed()
        {
            Finish();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }

        private async void Logout(object sender, EventArgs e)
        {
            bool ans = await core.Logout();
            if(ans)
            {
                Intent nextActivity = new Intent(this, typeof(LoginActivity));
                this.StartActivity(nextActivity);
            }
        }
    }
}
