using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Common.DBItems;
using DataAccess.ActivityData;

namespace CoreLogic.ActivityCore
{
    public class ActiveOrderCore
    {
        private Context context;
        private PendingOrdersData data;

        private List<Manboss_mandados_ruta> route;
        private Manboss_mandado order;

        public ActiveOrderCore(Context context)
        {
            this.context = context;
            data = new PendingOrdersData(context);
        }

        public async Task<Manboss_mandado> ActiveOrder()
        {
            int repartidorID = User.Repartidor.Id;
            order = await data.ActiveOrder(repartidorID, 3);
            return order;
        }

        public async Task<List<Manboss_mandados_ruta>> Route()
        {
            route = await data.Route(order.Id);
            return route;
        }
    }
}
