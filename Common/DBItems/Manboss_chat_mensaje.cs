using System;
using Newtonsoft.Json;

namespace Common.DBItems
{
    public class Manboss_chat_mensaje
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "mensaje")]
        public string Mensaje { get; set; }

        [JsonProperty(PropertyName = "chat")]
        public int Chat { get; set; }

        [JsonProperty(PropertyName = "rol")]
        public int Rol { get; set; }

    }
    public class Manboss_chat_mensajeWrapper : Java.Lang.Object
    {
        public Manboss_chat_mensaje Manboss_chat_mensaje { get; private set; }
        public Manboss_chat_mensajeWrapper(Manboss_chat_mensaje item)
        {
            Manboss_chat_mensaje = item;
        }
    }
}