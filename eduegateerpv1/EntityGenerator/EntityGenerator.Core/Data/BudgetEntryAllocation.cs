using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BudgetEntryAllocations", Schema = "budget")]
    public partial class BudgetEntryAllocation
    {
        [Key]
        public long BudgetEntryAllocationIID { get; set; }
        public long? BudgetEntryID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SuggestedStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SuggestedEndDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? SuggestedValue { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Percentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? EstimateValue { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }

        [ForeignKey("BudgetEntryID")]
        [InverseProperty("BudgetEntryAllocations")]
        public virtual BudgetEntry BudgetEntry { get; set; }
    }
}
