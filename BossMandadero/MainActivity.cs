using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Android.Content;
using CoreLogic;
using Android.Util;

namespace BossMandadero
{
    [Activity(Label = "BossMandadero", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/MainTheme", NoHistory = true)]
    public class MainActivity : Activity
    {
        private const int waitTime = 1000;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            PlayAnimation();

        }
        private async void PlayAnimation()
        {
            bool iniciado = await User.CheckIntegrity(this);
            Intent nextActivity;
            if(iniciado==true)
            {
                nextActivity = new Intent(this, typeof(Activities.ProfileActivity));
                StartActivity(nextActivity);
            }
            else
            {
                //Play the animation and wait time
                await Task.Delay(waitTime);

                //At the end it changes the activity from the welcome activiy to the login activity
                nextActivity = new Intent(this, typeof(Activities.LoginActivity));
                StartActivity(nextActivity); 
            }


        }

    }
}

