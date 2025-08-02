using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ProductDeliverySummaryView
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
    }
}
