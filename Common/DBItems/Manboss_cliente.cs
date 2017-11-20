using System;
using Newtonsoft.Json;

namespace Common.DBItems
{
    public class Manboss_cliente
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "correo")]
        public string Correo { get; set; }

        [JsonProperty(PropertyName = "telefono")]
        public string Telefono { get; set; }

        [JsonProperty(PropertyName = "red_social")]
        public string Red_social { get; set; }

        [JsonProperty(PropertyName = "direccion")]
        public string Direccion { get; set; }

        [JsonProperty(PropertyName = "latitud")]
        public double? Latitud { get; set; }

        [JsonProperty(PropertyName = "longitud")]
        public double? Longitud { get; set; }
    }
    public class Manboss_clienteWrapper : Java.Lang.Object
    {
        public Manboss_cliente Manboss_cliente { get; private set; }
        public Manboss_clienteWrapper(Manboss_cliente item)
        {
            Manboss_cliente = item;
        }
    }
}
