using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SupplierProductSummaryView
    {
        public int? TotalProducts { get; set; }
        public int OutOfStocK { get; set; }
        public int? InActive { get; set; }
        public int? UnderReview { get; set; }
        public int? DraftMode { get; set; }
        public int? TotalActiveProducts { get; set; }
        public int? TotalBrands { get; set; }
    }
}
