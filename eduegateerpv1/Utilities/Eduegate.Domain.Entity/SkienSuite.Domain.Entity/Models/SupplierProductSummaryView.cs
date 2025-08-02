using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SupplierProductSummaryView
    {
        public Nullable<int> TotalProducts { get; set; }
        public int OutOfStocK { get; set; }
        public Nullable<int> InActive { get; set; }
        public Nullable<int> UnderReview { get; set; }
        public Nullable<int> DraftMode { get; set; }
        public Nullable<int> TotalActiveProducts { get; set; }
        public Nullable<int> TotalBrands { get; set; }
    }
}
