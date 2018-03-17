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
        private List<Manboss_mandado> orders;
        private Manboss_mandado active;

        public int OrderID { get; set; }
        public PendingOrdersCore(Context context) {
            this.context = context;
            data = new PendingOrdersData(context);
        }

        public async Task<List<Manboss_mandado>> PendingOrders() {
            int repartidorID = User.Repartidor.Repartidor;
            orders = await data.PendingOrders(repartidorID, 2);
            active = await data.ActiveOrder(repartidorID, 3);

            if (active != null) {
                orders.Insert(0, active);
            }

            return orders;
        }

        public async Task<List<Manboss_mandados_ruta>> Route(int tipo)
        {
            route = await data.Route(OrderID,tipo);
            return route;
        }
        public async Task<bool> StartOrder(int OrderID)
        {
            return await data.SetOrder(OrderID, 3);
        }

    }
}
