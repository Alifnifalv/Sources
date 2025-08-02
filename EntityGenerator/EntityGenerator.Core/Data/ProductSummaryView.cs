using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ProductSummaryView
    {
        public int? TotalProducts { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string LastCreated { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string LastUpdated { get; set; }
        public int OutOfStocK { get; set; }
        public int? InActive { get; set; }
        public int? UnderReview { get; set; }
        public int? DraftMode { get; set; }
        public int? TotalActiveProducts { get; set; }
        public int? TotalActiveSKUs { get; set; }
        public int? TotalActiveSKUsOnline { get; set; }
    }
}
