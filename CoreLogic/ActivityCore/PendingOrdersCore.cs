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
    }
}
