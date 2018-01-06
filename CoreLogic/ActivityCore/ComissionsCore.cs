using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Common.DBItems;
using DataAccess.ActivityData;

namespace CoreLogic.ActivityCore
{
    public class ComissionsCore
    {

        private Context context;
        private ComissionsData data;

        public List<Manboss_comision> Comissions { get; set; }

        public List<string> Days = new List<string>() { "Día" };
        public List<string> Months = new List<string>() { "Mes", "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"};
        public List<string> Years = new List<string>() { "Año", "2016", "2017", "2018", "2019", "2020", "2021", "2022" };

        public ComissionsCore(Context context)
        {
            this.context = context;
            data = new ComissionsData(context);
        }

        public async Task<List<Manboss_comision>> GetComissions()
        {
            Comissions = await data.Comissions(User.Repartidor.Id);
            return Comissions;
        }
        public async Task<List<Manboss_comision>> Filter(int year, int month, int day)
        {
            if (year!=0)  year += 2015;
            Comissions = await data.Filter(User.Repartidor.Id, year, month, day);
            return Comissions;
        }
    }
}
