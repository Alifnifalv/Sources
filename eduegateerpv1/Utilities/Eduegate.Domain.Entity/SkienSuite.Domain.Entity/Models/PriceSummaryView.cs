using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PriceSummaryView
    {
        public Nullable<int> TotalPriceLists { get; set; }
        public int TotalActive { get; set; }
    }
}
