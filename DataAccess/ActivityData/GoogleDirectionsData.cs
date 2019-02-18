using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using Common;
using System.Threading.Tasks;
using Common.Utils;
using System.Net.Http;

namespace DataAccess.ActivityData
{
    public class GoogleDirectionsData
    {
        private MobileServiceClient client;
        private Context context;

        public GoogleDirectionsData(Context context)
        {
            this.context = context;
            client = new MobileServiceClient(GlobalValues.AppURL);
        }

        public async Task<string> GetPolyline(int MandadoID)
        {
            string answer = null;

            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "MandadoID", MandadoID.ToString() },
                }; 

                //GoogleDirections/GetPolyline
                var current = await client.InvokeApiAsync<string>("GoogleDirections/GetPolyline", HttpMethod.Post, param);
                answer = current;

            }
            catch (Exception e)
            {
                string m = e.Message;
                Dialogs.BasicDialog("No se pudo establecer conexión", "Error en al Red", context);
            }
            return answer;

        }
    }
}