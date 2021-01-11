using System.Collections.Generic;

namespace EFCory.Entities.KPIs
{
    public class KPI : Entity<int>
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        #region • Data •
        
        public static readonly KPI Profit = new KPI
        {
            Code = 10_100,
            Name = "Profit",
            ShortName = "Profit",
        };

        public static readonly KPI Cost = new KPI
        {
            Code = 10_101,
            Name = "Cost",
            ShortName = "",
        };

        public static readonly KPI SalesByRegion = new KPI
        {
            Code = 10_102,
            Name = "Sales By Region",
            ShortName = "SpR",
        };

        public static readonly KPI DSO = new KPI
        {
            Code = 10_103,
            Name = "Day Sales Outstanding ",
            ShortName = "DSO",
        };

        public static readonly KPI CLV = new KPI
        {
            Code = 11_100,
            Name = "Customer Lifetime Value",
            ShortName = "CLV",
        };

        public static readonly KPI CAC = new KPI
        {
            Code = 11_101,
            Name = "Customer Acquisition Cost",
            ShortName = "CAC",
        };


        public static readonly List<KPI> All = new List<KPI>
        {
            Profit, Cost, SalesByRegion, DSO,
            CLV, CAC
        };

        #endregion
    }
}
