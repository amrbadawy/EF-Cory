using System;

namespace EFCory.Entities.KPIs
{
    public class KpiItem : Entity<int>
    {
        public KpiAnnualItem KpiAnnualItem { get; set; }
        public int KpiAnnualItemId { get; set; }

        public KPI KPI { get; set; }
        public int KpiCode { get; set; }

        public double Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
