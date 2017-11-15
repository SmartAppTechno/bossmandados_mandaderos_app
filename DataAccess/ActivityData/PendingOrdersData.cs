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


        public async Task<List<Manboss_mandado>> PendingOrders(int repartidorID)
        {
            List<Manboss_mandado> mandados = new List<Manboss_mandado>();

            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "RepartidorID", repartidorID.ToString() },
                    { "Estado", "2" }
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
    }
}
