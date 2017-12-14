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
    public class ChatData
    {
        private MobileServiceClient client;
        private Context context;
        public ChatData(Context context)
        {
            this.context = context;
            client = new MobileServiceClient(GlobalValues.AppURL);
        }

        public async Task<int> Chat(int mandadoID, int repartidorID)
        {
            int ans = 0;
            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "MandadoID", mandadoID.ToString() },
                    { "RepartidorID", repartidorID.ToString() }
                };
                var current = await client.InvokeApiAsync<int>("Chat", HttpMethod.Post, param);
                ans = current;

            }
            catch (Exception e)
            {
            }
            return ans;
        }
        public async Task<bool> Mensaje(int chatID, string message)
        {
            bool ans = false;
            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "ChatID", chatID.ToString() },
                    { "Mensaje", message }
                };
                var current = await client.InvokeApiAsync<bool>("Chat", HttpMethod.Post, param);
                ans = current;

            }
            catch (Exception e)
            {
            }
            return ans;
        }
        public async Task<List<Manboss_chat_mensaje>> Conversacion(int mandadoID)
        {
            List<Manboss_chat_mensaje> ans = null;
            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "MandadoID", mandadoID.ToString() }
                };
                var current = await client.InvokeApiAsync<List<Manboss_chat_mensaje>>("Chat", HttpMethod.Post, param);
                ans = current;

            }
            catch (Exception e)
            {
            }
            return ans;
        }
    }
}
