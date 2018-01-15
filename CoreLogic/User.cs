using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Locations;
using Android.Preferences;
using Common.DBItems;
using DataAccess.ActivityData;

namespace CoreLogic
{
    public class User
    {
        public static Manboss_usuario Usuario { get; set; }
        public static Manboss_repartidor Repartidor { get; set; }
        public static bool CanSetUbicacion { get; set; }

        public static async Task<bool> CheckIntegrity(Context context)
        {
            CanSetUbicacion = true;
            if (Usuario == null)
            {
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(context);
                int UserID = prefs.GetInt("BossMandados_UserID", 0);

                if(UserID == 0)
                {
                    return false;
                }

                LoginData logindata = new LoginData(context);

                Usuario = await logindata.GetUser(UserID);

            }

            if(Repartidor == null)
            {
                WelcomeData welcomedata = new WelcomeData(context);
                Repartidor = await welcomedata.Repartidor(Usuario.Id);
            }
            return Repartidor != null;
        }

        public static bool Logout(Context context)
        {
            CanSetUbicacion = false;
            try
            {
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(context);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.Remove("BossMandados_UserID");
                editor.Apply();
                Usuario = null;
                Repartidor = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async void UpdateLocation(Location location, Context context)
        {
            CanSetUbicacion = false;

            WelcomeData data = new WelcomeData(context);
            await data.SetUbicacion(location.Latitude, location.Longitude, Repartidor.Id);
            await Task.Delay(10000);

            CanSetUbicacion = true;
        }


    }
}
