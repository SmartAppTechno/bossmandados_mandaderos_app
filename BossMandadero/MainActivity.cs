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
        public const string TAG = "Mandadero";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

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

