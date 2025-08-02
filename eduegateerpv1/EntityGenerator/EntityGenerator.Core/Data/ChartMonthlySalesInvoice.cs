using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ChartMonthlySalesInvoice
    {
        public int? Years { get; set; }
        [Column("1", TypeName = "decimal(38, 3)")]
        public decimal? _1 { get; set; }
        [Column("2", TypeName = "decimal(38, 3)")]
        public decimal? _2 { get; set; }
        [Column("3", TypeName = "decimal(38, 3)")]
        public decimal? _3 { get; set; }
        [Column("4", TypeName = "decimal(38, 3)")]
        public decimal? _4 { get; set; }
        [Column("5", TypeName = "decimal(38, 3)")]
        public decimal? _5 { get; set; }
        [Column("6", TypeName = "decimal(38, 3)")]
        public decimal? _6 { get; set; }
        [Column("7", TypeName = "decimal(38, 3)")]
        public decimal? _7 { get; set; }
        [Column("8", TypeName = "decimal(38, 3)")]
        public decimal? _8 { get; set; }
        [Column("9", TypeName = "decimal(38, 3)")]
        public decimal? _9 { get; set; }
        [Column("10", TypeName = "decimal(38, 3)")]
        public decimal? _10 { get; set; }
        [Column("11", TypeName = "decimal(38, 3)")]
        public decimal? _11 { get; set; }
        [Column("12", TypeName = "decimal(38, 3)")]
        public decimal? _12 { get; set; }
    }
}
