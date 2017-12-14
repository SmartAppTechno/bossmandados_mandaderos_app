using System;
using System.Threading.Tasks;
using Android.Content;
using DataAccess.ActivityData;

namespace CoreLogic.ActivityCore
{
    public class ProfileCore
    {
        private Context context;
        private WelcomeData data;
        public ProfileCore(Context context)
        {
            this.context = context;
            data = new WelcomeData(context);
        }

        public async Task<bool> Logout()
        {
            bool result = await data.SetStatus(false, User.Repartidor.Id);
            if(result)
            {
                User.Logout(context);
            }
            return result;
        }
    }
}
