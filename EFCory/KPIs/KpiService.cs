using EFCory.Entities.KPIs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UPD.EntityFramework;
using System.Linq;

namespace EFCory.KPIs
{
    public class KpiService : AppService, IKpiService
    {
        #region • Init •

        public KpiService(DatabaseContext databaseContext) : base(databaseContext) { }

        #endregion

        public async Task Test()
        {
            await _db.ResetDatabase();
            await InitData();

            //await AddPostTags();
        }

        public async Task InitData()
        {
            _db.KPIs.AddRange(KPI.All);
            await _db.SaveChangesAsync();

            var profitKpi = new KpiItem
            {
                KPI = KPI.Profit,
                Value = 12.35
            };

            var costKpi = new KpiItem
            {
                KPI = KPI.Cost,
                Value = 98765.32
            };

            var record = new KpiAnnualItem
            {
                Year = 2021,
                Description = "2021 Desription",
                KpiItems = { profitKpi, costKpi }
            };

            _db.KpiAnnualItems.Add(record);

            await _db.SaveChangesAsync();
        }
    }
}