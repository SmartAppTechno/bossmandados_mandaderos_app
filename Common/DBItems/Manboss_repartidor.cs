using System;
using Newtonsoft.Json;
namespace Common.DBItems
{
    public class Manboss_repartidor
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public double Rating { get; set; }

        [JsonProperty(PropertyName = "efectivo")]
        public double Efectivo { get; set; }

        [JsonProperty(PropertyName = "repartidor")]
        public int Repartidor { get; set; }

        [JsonProperty(PropertyName = "direccion")]
        public string Direccion { get; set; }


    }
    public class Manboss_repartidorWrapper : Java.Lang.Object
    {
        public Manboss_repartidor Manboss_repartidor { get; private set; }
        public Manboss_repartidorWrapper(Manboss_repartidor item)
        {
            Manboss_repartidor = item;
        }
    }
}
