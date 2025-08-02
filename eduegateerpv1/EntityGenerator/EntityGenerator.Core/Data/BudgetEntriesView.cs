using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class BudgetEntriesView
    {
        public int? BudgetID { get; set; }
        [StringLength(100)]
        public string BudgetName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PeriodStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PeriodEnd { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? BudgettedValue { get; set; }
    }
}
