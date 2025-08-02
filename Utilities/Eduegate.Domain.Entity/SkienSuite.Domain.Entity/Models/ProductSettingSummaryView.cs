using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductSettingSummaryView
    {
        public Nullable<int> TotalProducts { get; set; }
        public string LastCreated { get; set; }
        public string LastUpdated { get; set; }
        public int OutOfStocK { get; set; }
        public Nullable<int> InActive { get; set; }
        public Nullable<int> UnderReview { get; set; }
        public Nullable<int> DraftMode { get; set; }
    }
}
