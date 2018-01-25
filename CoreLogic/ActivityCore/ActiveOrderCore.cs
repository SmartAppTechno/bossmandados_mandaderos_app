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

        public List<Manboss_mandados_ruta> route { get; set; }
        private Manboss_mandado order;
        public Manboss_cliente Client { get; set; }

        public ActiveOrderCore(Context context)
        {
            this.context = context;
            data = new PendingOrdersData(context);
        }

        public async Task<Manboss_mandado> ActiveOrder()
        {
            int repartidorID = User.Repartidor.Id;
            order = await data.ActiveOrder(repartidorID, 3);

            if(order != null)
            {
                Client = await data.Client(order.Cliente);
            }

            return order;
        }

        public async Task<List<Manboss_mandados_ruta>> Route(int tipo)
        {
            route = await data.Route(order.Id, tipo);
            return route;
        }
        public async Task<bool> CompleteTask(Manboss_mandados_ruta r)
        {
            if(route.Count == 1)
            {
                ComissionsData cData = new ComissionsData(context);

                await data.SetOrder(order.Id, 4);
                await cData.AddComission(User.Repartidor.Id, order.Id);

            }
            return await data.CompleteTask(r.Id);
        }
        public async Task<Manboss_cliente> GetClient(Manboss_mandado order)
        {
            this.order = order;
            Client = await data.Client(order.Cliente);
            return Client;
        }
    }
}
