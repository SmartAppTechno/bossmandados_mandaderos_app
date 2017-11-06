
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

namespace BossMandadero.Activities
{
    [Activity(Label = "ProfileActivity", Theme = "@style/AppDrawerTheme")]
    public class ProfileActivity : AppCompatActivity
    {

        Drawer drawer;

        TextView name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProfileLayout);

            drawer = new Drawer(this);
            SetResources();
        }

        private void SetResources()
        {
            //Get reference to the needed resources
            name = FindViewById<TextView>(Resource.Id.txt_Name);

            //Set the button methods
            name.Text = User.Usuario.Nombre;
        }

        public override void OnBackPressed()
        {
            Finish();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}
