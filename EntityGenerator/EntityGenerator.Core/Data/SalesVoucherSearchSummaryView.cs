using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SalesVoucherSearchSummaryView
    {
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? ToalSalesAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? TodaysAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? YesterdayAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? LastWeekAmount { get; set; }
    }
}
