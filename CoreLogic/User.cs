using System;
using Common.DBItems;

namespace CoreLogic
{
    public class User
    {
        public static Manboss_usuario Usuario { get; set; }
        public static Manboss_repartidor Repartidor { get; set; }

        public static bool CheckIntegrity()
        {
            if (Usuario == null)
            {
                //TODO GetUser
            }

            return Repartidor != null;
        }
    }
}
