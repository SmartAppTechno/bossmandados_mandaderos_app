using System;
using Newtonsoft.Json;

namespace Common.DBItems
{
    public class Manboss_mandados_ruta
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "mandado")]
        public int Mandado { get; set; }

        [JsonProperty(PropertyName = "servicio")]
        public int Servicio { get; set; }

        [JsonProperty(PropertyName = "latitud")]
        public double Latitud { get; set; }

        [JsonProperty(PropertyName = "longitud")]
        public double Longitud { get; set; }

        [JsonProperty(PropertyName = "calle")]
        public string Calle { get; set; }

        [JsonProperty(PropertyName = "numero")]
        public int? Numero { get; set; }

        [JsonProperty(PropertyName = "comentarios")]
        public string Comentarios { get; set; }

        [JsonProperty(PropertyName = "tamanio")]
        public int? Tamanio { get; set; }

        [JsonProperty(PropertyName = "peso")]
        public double? Peso { get; set; }
    }
}
