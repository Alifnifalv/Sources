using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PriceSearchView
    {
        public string PriceDescription { get; set; }
        public long ProductPriceListIID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> PricePercentage { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string PriceListTypeName { get; set; }
        public string Name { get; set; }
    }
}
