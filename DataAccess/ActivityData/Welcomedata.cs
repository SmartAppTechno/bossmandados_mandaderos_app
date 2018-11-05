using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Content;
using Common;
using Common.DBItems;
using Common.Utils;
using Microsoft.WindowsAzure.MobileServices;

namespace DataAccess.ActivityData {
    public class WelcomeData {
        private MobileServiceClient client;
        private IMobileServiceTable<Manboss_usuario> userTable;
        private Context context;
        public WelcomeData(Context context) {
            this.context = context;
            client = new MobileServiceClient(GlobalValues.AppURL);
            userTable = client.GetTable<Manboss_usuario>();
        }


        public async Task<Manboss_repartidor> Repartidor(int RepartidorID) {
            Manboss_repartidor userReturn = null;

            try {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "RepartidorID", RepartidorID.ToString() },
                    { "MetodoRepartidor", "true" }
                };
                var current = await client.InvokeApiAsync<Manboss_repartidor>("Perfil/Repartidor", HttpMethod.Post, param);
                userReturn = current;

            }
            catch (Exception e) {
                string m = e.Message;
                Dialogs.BasicDialog("No se pudo establecer conexión", "Error en al Red", context);
            }
            return userReturn;
        }

        public async Task<bool> SetEfectivo(double efectivo, int RepartidorID) {
            bool result = false;

            try {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "Efectivo", efectivo.ToString() },
                    { "RepartidorID", RepartidorID.ToString() }
                };
                result = await client.InvokeApiAsync<bool>("Repartidor/CantidadEfectivo", HttpMethod.Post, param);

            }
            catch (Exception e) {
                string m = e.Message;
                Dialogs.BasicDialog("No se pudo establecer conexión", "Error en al Red", context);
            }
            return result;
        }
        public async Task<int> SetUbicacion(double latitud, double longitud, int RepartidorID) {
            int result = 0;

            try {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "Latitud", latitud.ToString() },
                    { "Longitud", longitud.ToString() },
                    { "RepartidorID", RepartidorID.ToString()}
                };
                result = await client.InvokeApiAsync<int>("Repartidor/Ubicacion", HttpMethod.Post, param);

            }
            catch (Exception e) {
                string m = e.Message;
                Dialogs.BasicDialog("No se pudo establecer conexión", "Error en al Red", context);
            }
            return result;
        }
        public async Task<bool> SetStatus(bool status, int RepartidorID) {
            bool result = false;

            try {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "Estado", status.ToString() },
                    { "RepartidorID", RepartidorID.ToString() }
                };
                result = await client.InvokeApiAsync<bool>("Repartidor/Estado", HttpMethod.Post, param);

            }
            catch (Exception e) {
                string m = e.Message;
                Dialogs.BasicDialog("No se pudo establecer conexión", "Error en al Red", context);
            }
            return result;
        }
    }
}
