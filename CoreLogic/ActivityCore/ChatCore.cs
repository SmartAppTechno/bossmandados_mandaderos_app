using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Common.DBItems;
using DataAccess.ActivityData;

namespace CoreLogic.ActivityCore
{
    public class ChatCore
    {
        private ChatData data;
        private Activity act;
        private int mandadoID;
        private int chatID;
        public List<Manboss_chat_mensaje> Mensajes { get; set; }

        public string NombreCliente { get; set; }

        public ChatCore(Activity activity, int mandadoID)
        {
            this.act = activity;
            this.mandadoID = mandadoID;
            data = new ChatData(activity);
        }

        public async Task<int> Chat()
        {
            chatID = await data.Chat(mandadoID, User.Repartidor.Id);
            return chatID;
        }
        public async Task<bool> Message(string message)
        {
            return await data.Mensaje(chatID, message);
        }
        public async Task<List<Manboss_chat_mensaje>> Conversation()
        {
            Mensajes = await data.Conversacion(mandadoID);
            return Mensajes;
        }
    }
}
