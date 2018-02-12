using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Content;
using Common;
using Common.DBItems;
using Common.Utils;
using Microsoft.WindowsAzure.MobileServices;

namespace DataAccess.ActivityData
{
    public class ComissionsData
    {
        private MobileServiceClient client;
        private Context context;
        public ComissionsData(Context context)
        {
            this.context = context;
            client = new MobileServiceClient(GlobalValues.AppURL);
        }

        public async Task<List<Manboss_comision>> Comissions(int repartidorID)
        {
            List<Manboss_comision> ans = null;
            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "RepartidorID", repartidorID.ToString() }
                };
                var current = await client.InvokeApiAsync<List<Manboss_comision>>("Comision", HttpMethod.Post, param);
                ans = current;

            }
            catch (Exception e)
            {
                Dialogs.BasicDialog("No se pudo establecer conexión", "Error en al Red", context);
            }
            return ans;
        }
        public async Task<List<Manboss_comision>> Filter(int repartidorID, int year, int month, int day)
        {
            List<Manboss_comision> ans = null;
            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "RepartidorID", repartidorID.ToString() },
                    { "Day", day.ToString() },
                    { "Month", month.ToString() },
                    { "Year", year.ToString() },
                };
                var current = await client.InvokeApiAsync<List<Manboss_comision>>("Comision", HttpMethod.Post, param);
                ans = current;

            }
            catch (Exception e)
            {
                Dialogs.BasicDialog("No se pudo establecer conexión", "Error en al Red", context);
            }
            return ans;
        }
        public async Task<bool> AddComission(int repartidorID, int mandadoID)
        {
            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "RepartidorID", repartidorID.ToString() },
                    { "MandadoID", mandadoID.ToString() }
                };
                return await client.InvokeApiAsync<bool>("Comision", HttpMethod.Post, param);

            }
            catch (Exception e)
            {
                Dialogs.BasicDialog("No se pudo establecer conexión", "Error en al Red", context);
            }
            return false;
        }

        public async Task<Manboss_mandado> GetMandado(int mandadoID)
        {
            Manboss_mandado mandado = null;
            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "MandadoID", mandadoID.ToString() }
                };
                mandado =  await client.InvokeApiAsync<Manboss_mandado>("Mandados", HttpMethod.Post, param);

            }
            catch (Exception e)
            {
                Dialogs.BasicDialog("No se pudo establecer conexión", "Error en al Red", context);
            }
            return mandado;
        }
    }
}
