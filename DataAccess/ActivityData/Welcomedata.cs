using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Content;
using Common;
using Common.DBItems;
using Microsoft.WindowsAzure.MobileServices;

namespace DataAccess.ActivityData
{
    public class WelcomeData
    {
        private MobileServiceClient client;
        private IMobileServiceTable<Manboss_usuario> userTable;
        private Context context;
        public WelcomeData(Context context)
        {
            this.context = context;
            client = new MobileServiceClient(GlobalValues.AppURL);
            userTable = client.GetTable<Manboss_usuario>();
        }


        public async Task<Manboss_repartidor> Repartidor(int RepartidorID)
        {
            Manboss_repartidor userReturn = null;

            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "RepartidorID", RepartidorID.ToString() },
                    { "MetodoRepartidor", "true" }
                };
                var current = await client.InvokeApiAsync<Manboss_repartidor>("Perfil", HttpMethod.Post, param);
                userReturn = current;

            }
            catch (Exception e)
            {
                
            }
            return userReturn;
        }

        public async Task<bool> SetEfectivo(double efectivo, int RepartidorID)
        {
            bool result = false;

            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "Efectivo", efectivo.ToString() },
                    { "RepartidorID", RepartidorID.ToString() }
                };
                result = await client.InvokeApiAsync<bool>("Repartidor", HttpMethod.Post, param);

            }
            catch (Exception e)
            {

            }
            return result;
        }
        public async Task<bool> SetUbicacion(double latitud, double longitud, int RepartidorID)
        {
            bool result = false;

            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "Latitud", latitud.ToString() },
                    { "Longitud", longitud.ToString() },
                    { "RepartidorID", RepartidorID.ToString()}
                };
                result = await client.InvokeApiAsync<bool>("Repartidor", HttpMethod.Post, param);

            }
            catch (Exception e)
            {

            }
            return result;
        }
        public async Task<bool> SetStatus(bool status, int RepartidorID)
        {
            bool result = false;

            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "Estado", status.ToString() },
                    { "RepartidorID", RepartidorID.ToString() }
                };
                result = await client.InvokeApiAsync<bool>("Repartidor", HttpMethod.Post, param);

            }
            catch (Exception e)
            {

            }
            return result;
        }
    }
}
