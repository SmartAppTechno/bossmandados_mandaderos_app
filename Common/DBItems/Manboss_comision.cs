using System;
using Newtonsoft.Json;

namespace Common.DBItems
{
    public class Manboss_comision
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "repartidor")]
        public int Repartidor { get; set; }

        [JsonProperty(PropertyName = "mandado")]
        public int Mandado { get; set; }

        [JsonProperty(PropertyName = "comision")]
        public int Comision { get; set; }


    }
    public class Manboss_comisionWrapper : Java.Lang.Object
    {
        public Manboss_comision Manboss_comision { get; private set; }
        public Manboss_comisionWrapper(Manboss_comision item)
        {
            Manboss_comision = item;
        }
    }
}
