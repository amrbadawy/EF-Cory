namespace EFCory.Entities.KPIs
{
    public class KpiItem : Entity<int>
    {
        public KPI KPI { get; set; }
        public int KpiId { get; set; }

        public double Value { get; set; }
    }

}
