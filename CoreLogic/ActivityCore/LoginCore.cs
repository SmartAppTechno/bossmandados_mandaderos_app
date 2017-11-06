using System;
using System.Threading.Tasks;
using Android.Content;
using Common.DBItems;
using DataAccess.ActivityData;

namespace CoreLogic.ActivityCore
{
    public class LoginCore
    {
        private Context context;
        private LoginData data;
        public LoginCore(Context context)
        {
            this.context = context;
            data = new LoginData(context);
        }

        public async Task<Manboss_usuario> Register(string email, string password)
        {
            Manboss_usuario user = await data.Login(email, password);
            User.Usuario = user;
            return user;
        }
    }
}
