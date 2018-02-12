using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Common;
using Common.DBItems;
using Common.Utils;
using Microsoft.WindowsAzure.MobileServices;


namespace DataAccess.ActivityData
{
    public class LoginData
    {
        private MobileServiceClient client;
        private IMobileServiceTable<Manboss_usuario> userTable;
        private Context context;
        public LoginData(Context context)
        {
            this.context = context;
            client = new MobileServiceClient(GlobalValues.AppURL);
            userTable = client.GetTable<Manboss_usuario>();
        }


        public async Task<Manboss_usuario> Login(string correo, string password)
        {
            Manboss_usuario userReturn = null;

            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "correo", correo },
                    { "password", password }
                };
                var current = await client.InvokeApiAsync<Manboss_usuario>("Repartidor", HttpMethod.Post, param);
                userReturn = current;

            }
            catch (Exception e)
            {
                Dialogs.BasicDialog("No se pudo establecer conexión","Error en al Red",context);
            }
            return userReturn;
        }

        public async Task<Manboss_usuario> GetUser(int UserID)
        {
            Manboss_usuario userReturn = null;

            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "UsuarioID", UserID.ToString() }
                };
                var current = await client.InvokeApiAsync<Manboss_usuario>("Usuario", HttpMethod.Post, param);
                userReturn = current;

            }
            catch (Exception e)
            {
                Dialogs.BasicDialog("No se pudo establecer conexión", "Error en al Red", context);
            }
            return userReturn;
        }
        private void CreateAndShowDialog(Exception exception, String title)
        {
            CreateAndShowDialog(exception.Message, title);
        }

        private void CreateAndShowDialog(string message, string title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(context);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }


    }
}
