using System;
using System.Threading.Tasks;
using Android.Content;
using Common.DBItems;
using DataAccess.ActivityData;

namespace CoreLogic.ActivityCore
{
    public class WelcomeCore
    {
        private Context context;
        private WelcomeData data;

        public WelcomeCore(Context context)
        {
            this.context = context;
            this.data = new WelcomeData(context);

        }

        public async Task<double> CheckIntegrity(){
            await User.CheckIntegrity(context);
            return User.Repartidor.Efectivo;
        }

        public async Task<Manboss_repartidor> Repartidor(int RepartidorID)
        {
            Manboss_repartidor repartidor = await data.Repartidor(RepartidorID);
            User.Repartidor = repartidor;
            return repartidor;
        }

        public async Task<bool> StartDay(double efectivo){

            bool resultEfectivo = await data.SetEfectivo(efectivo, User.Repartidor.Id);
            bool resultStatus = await data.SetStatus( true, User.Repartidor.Id );


            if (resultEfectivo)
            {
                User.Repartidor.Efectivo = efectivo;
            }

            return resultEfectivo && resultStatus;
        }
    }
}
