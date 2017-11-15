using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Common.DBItems;
using DataAccess.ActivityData;

namespace CoreLogic.ActivityCore
{
    public class PendingOrdersCore
    {
        private Context context;
        private PendingOrdersData data;

        private List<Manboss_mandados_ruta> route;

        public int OrderID { get; set; }


        public PendingOrdersCore(Context context)
        {
            this.context = context;
            data = new PendingOrdersData(context);
        }

        public async Task<List<Manboss_mandado>> PendingOrders()
        {
            int repartidorID = User.Repartidor.Id;
            return await data.PendingOrders(repartidorID);
        }

        public async Task<List<Manboss_mandados_ruta>> Route()
        {
            route = await data.Route(OrderID);
            return route;
        }
    }
}
