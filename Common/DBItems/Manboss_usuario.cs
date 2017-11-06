using System;
using Newtonsoft.Json;
namespace Common.DBItems
{
    public class Manboss_usuario
    {

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "correo")]
        public string Correo { get; set; }

        [JsonProperty(PropertyName = "usuario")]
        public string Usuario { get; set; }
    }

    public class Manboss_usuarioWrapper : Java.Lang.Object
    {
        public Manboss_usuario Manboss_usuario { get; private set; }
        public Manboss_usuarioWrapper(Manboss_usuario item)
        {
            Manboss_usuario = item;
        }
    }
}
