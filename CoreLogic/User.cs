using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Preferences;
using Common.DBItems;
using DataAccess.ActivityData;

namespace CoreLogic
{
    public class User
    {
        public static Manboss_usuario Usuario { get; set; }
        public static Manboss_repartidor Repartidor { get; set; }

        public static async Task<bool> CheckIntegrity(Context context)
        {
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
            try
            {
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(context);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.Remove("BossMandados_UserID");
                editor.Apply();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
