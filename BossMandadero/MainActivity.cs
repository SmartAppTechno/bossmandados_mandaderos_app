using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Android.Content;

namespace BossMandadero
{
    [Activity(Label = "BossMandadero", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/MainTheme")]
    public class MainActivity : Activity
    {
        private const int waitTime = 2000;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            PlayAnimation();

        }
        private async void PlayAnimation()
        {

            //Play the animation and wait time
            await Task.Delay(waitTime);

            //At the end it changes the activity from the welcome activiy to the login activity
            Intent nextActivity = new Intent(this, typeof(Activities.LoginActivity));
            StartActivity(nextActivity);

        }
    }
}

