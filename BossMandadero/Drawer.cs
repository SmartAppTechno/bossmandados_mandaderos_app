using System;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Widget;
using System.Collections.Generic;
using Common.Items;
using Android.Views;
using static Android.Widget.AdapterView;
using Android.Content;
using BossMandadero.Activities;
using Android.Graphics;

namespace BossMandadero
{
    public class Drawer
    {
        private AppCompatActivity activity;

        private SupportToolbar mToolbar;
        private DrawerLayout mDrawerLayout;
        private ActionBarDrawerToggle mDrawerToggle;
        private List<DrawerElement> arrayList;
        private ListView drawer;

        public Drawer(AppCompatActivity activity)
        {
            
            this.activity = activity;

            mToolbar = activity.FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = (DrawerLayout)activity.FindViewById(Resource.Id.drawer_layout);
            activity.SetSupportActionBar(mToolbar);
            activity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            mDrawerToggle = new ActionBarDrawerToggle(activity, mDrawerLayout, mToolbar, Resource.String.login_password, Resource.String.login_email);
            mDrawerToggle.SyncState();

            drawer = activity.FindViewById<ListView>(Resource.Id.drawer);

            Elements();

        }

        public void Elements(){

            arrayList = new List<DrawerElement>();

            arrayList.Add(new DrawerElement("Status", DrawerPosition.Status));
            arrayList.Add(new DrawerElement("Perfil", DrawerPosition.Perfil));

            DrawerAdapter adapter = new DrawerAdapter(arrayList, activity);
            drawer.Adapter = adapter;

            drawer.ItemClick += OnItemClick;

        }

        public void OnItemClick(object sender, ItemClickEventArgs e)
        {

            DrawerPosition position = arrayList[e.Position].DrawerPosition;
            Intent nextActivity;
            switch(position){
                case DrawerPosition.Perfil:
                    nextActivity = new Intent(activity, typeof(ProfileActivity));
                    activity.StartActivity(nextActivity);
                    break;
                case DrawerPosition.Status:
                    nextActivity = new Intent(activity, typeof(WelcomeActivity));
                    activity.StartActivity(nextActivity);
                    break;
            }

        }

        public View GetView(int position, View convertView, ViewGroup parent){

            convertView.SetBackgroundColor(new Color(Color.Red));

            return convertView;
        }
    }
}
