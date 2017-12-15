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

        public List<int> Days = new List<int>();
        public List<int> Months = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        public List<int> Years = new List<int>() { 0, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025, 2026 };

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
