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
    public class PendingOrdersData
    {
        private MobileServiceClient client;
        private Context context;
        public PendingOrdersData(Context context)
        {
            this.context = context;
            client = new MobileServiceClient(GlobalValues.AppURL);
        }


        public async Task<List<Manboss_mandado>> PendingOrders(int repartidorID, int estado)
        {
            List<Manboss_mandado> mandados = new List<Manboss_mandado>();

            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "RepartidorID", repartidorID.ToString() },
                    { "Estado", estado.ToString() }
                };
                mandados = await client.InvokeApiAsync<List<Manboss_mandado>>("MandadosActivos", HttpMethod.Post, param);

            }
            catch (Exception e)
            {
            }
            return mandados;
        }

        public async Task<List<Manboss_mandados_ruta>> Route(int orderID)
        {
            List<Manboss_mandados_ruta> ruta = new List<Manboss_mandados_ruta>();

            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>()
                {
                    { "MandadoID", orderID.ToString() }
                };
                ruta = await client.InvokeApiAsync<List<Manboss_mandados_ruta>>("MandadosActivos", HttpMethod.Post, param);
            }
            catch(Exception e)
            {}
            return ruta;
        }
        public async Task<Manboss_mandado> ActiveOrder(int repartidorID, int estado)
        {
            Manboss_mandado mandado = null;

            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "RepartidorID", repartidorID.ToString() },
                    { "Estado", estado.ToString() }
                };
                var mandados = await client.InvokeApiAsync<List<Manboss_mandado>>("MandadosActivos", HttpMethod.Post, param);
                mandado = mandados[0];
            }
            catch (Exception e)
            {
            }
            return mandado;
        }
        public async Task<Manboss_cliente> Client(int clientID)
        {
            Manboss_cliente cliente = null;

            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "ClienteID", clientID.ToString() }
                };
                var current = await client.InvokeApiAsync<Manboss_cliente>("Cliente", HttpMethod.Post, param);
                cliente = current;
            }
            catch (Exception e)
            {
            }
            return cliente;
        }
        public async Task<bool> SetOrder(int MandadoID, int State)
        {
            return true;
        }
    }
}
