using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Budgeting.Models
{
    [Table("BudgetEntryAllocations", Schema = "budget")]
    public partial class BudgetEntryAllocation
    {
        [Key]
        public long BudgetEntryAllocationIID { get; set; }

        public long? BudgetEntryID { get; set; }

        public DateTime? SuggestedStartDate { get; set; }

        public DateTime? SuggestedEndDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal? SuggestedValue { get; set; }

        public decimal? Percentage { get; set; }

        public decimal? EstimateValue { get; set; }

        public decimal? Amount { get; set; }

        public virtual BudgetEntry BudgetEntry { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

    }
}