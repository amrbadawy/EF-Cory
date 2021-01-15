using EFCory.Entities.KPIs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UPD.EntityFramework;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System;

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

            var items = new List<KpiItem>
            {
                new KpiItem
                {
                    KpiCode = KPI.Profit.Code,
                    Value = 15.20
                },
                new KpiItem
                {
                    KpiCode = KPI.Cost.Code,
                    Value = 255.30
                },
                new KpiItem
                {
                    KpiCode = KPI.CLV.Code,
                    Value = 2695.23
                }
            };
            await AddOrUpdateKpiItems(items);


            var newItems = new List<KpiItem>
            {
                new KpiItem
                {
                    KpiCode = KPI.Profit.Code,
                    Value = 45
                },
                new KpiItem
                {
                    KpiCode = KPI.Cost.Code,
                    Value = 625
                },
                new KpiItem
                {
                    KpiCode = KPI.DSO.Code,
                    Value = 4589
                }
            };
            await AddOrUpdateKpiItems(newItems);
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
                //KpiItems = { profitKpi, costKpi }
            };

            _db.KpiAnnualItems.Add(record);

            await SaveChangesAsync();
        }

        public async Task AddOrUpdateKpiItems([DisallowNull] List<KpiItem> kpiItems)
        {
            var annualItem = await _db.KpiAnnualItems
                .Include(a => a.KpiItems)
                .FirstOrDefaultAsync(a => a.Year == 2021);

            foreach (var item in annualItem.KpiItems)
            {
                var newItem = kpiItems.Find(k => k.KpiCode == item.KpiCode);
                if (newItem is not null)
                {
                    item.Value = newItem.Value;
                    item.UpdatedAt = DateTime.Now;
                }
            }

            var itemComparer = new KpiItemEqualityComparer();

            var deletedItems = annualItem.KpiItems.Except(kpiItems, itemComparer).ToList();
            deletedItems.ForEach(item => annualItem.KpiItems.Remove(item));

            var addedItems = kpiItems.Except(annualItem.KpiItems, itemComparer).ToList();
            addedItems.ForEach(item => annualItem.KpiItems.Add(item));

            await SaveChangesAsync();
        }

        class KpiItemEqualityComparer : IEqualityComparer<KpiItem>
        {
            public bool Equals(KpiItem x, KpiItem y)
            {
                return x.KpiCode == y.KpiCode;
            }

            public int GetHashCode(KpiItem obj)
            {
                return obj.KpiCode.GetHashCode();
            }
        }
    }
}