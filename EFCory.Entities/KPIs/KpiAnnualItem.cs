using System;
using System.Collections.Generic;

namespace EFCory.Entities.KPIs
{
    public class KpiAnnualItem : Entity<int>
    {
        public int Year { get; set; }
        public string Description { get; set; }

        public ICollection<KpiItem> KpiItems { get; set; } = new List<KpiItem>();
        public DateTime CreatedAt { get; set; }
    }
}
