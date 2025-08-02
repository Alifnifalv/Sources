using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ChartMonthlyJobCount
    {
        public int? Years { get; set; }
        [Column("1")]
        public int? _1 { get; set; }
        [Column("2")]
        public int? _2 { get; set; }
        [Column("3")]
        public int? _3 { get; set; }
        [Column("4")]
        public int? _4 { get; set; }
        [Column("5")]
        public int? _5 { get; set; }
        [Column("6")]
        public int? _6 { get; set; }
        [Column("7")]
        public int? _7 { get; set; }
        [Column("8")]
        public int? _8 { get; set; }
        [Column("9")]
        public int? _9 { get; set; }
        [Column("10")]
        public int? _10 { get; set; }
        [Column("11")]
        public int? _11 { get; set; }
        [Column("12")]
        public int? _12 { get; set; }
    }
}
