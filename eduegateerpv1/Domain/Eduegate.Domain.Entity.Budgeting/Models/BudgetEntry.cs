using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Budgeting.Models
{
    [Table("BudgetEntries", Schema = "budget")]
    public partial class BudgetEntry
    {
        public BudgetEntry()
        {
            BudgetEntryAccountMaps = new HashSet<BudgetEntryAccountMap>();
            BudgetEntryAllocations = new HashSet<BudgetEntryAllocation>();
            BudgetEntryCostCenterMaps = new HashSet<BudgetEntryCostCenterMap>();
            BudgetEntryFeeMaps = new HashSet<BudgetEntryFeeMap>();
        }

        [Key]
        public long BudgetEntryIID { get; set; }

        public int? BudgetID { get; set; }

        public byte? BudgetSuggestionID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public decimal? SuggestedValue { get; set; }

        public decimal? Percentage { get; set; }

        public decimal? Amount { get; set; }

        public decimal? EstimateValue { get; set; }

        public DateTime? SuggestedStartDate { get; set; }

        public DateTime? SuggestedEndDate { get; set; }

        public virtual Budget1 Budget { get; set; }

        public virtual BudgetSuggestion BudgetSuggestion { get; set; }

        public virtual ICollection<BudgetEntryAccountMap> BudgetEntryAccountMaps { get; set; }

        public virtual ICollection<BudgetEntryAllocation> BudgetEntryAllocations { get; set; }

        public virtual ICollection<BudgetEntryCostCenterMap> BudgetEntryCostCenterMaps { get; set; }

        public virtual ICollection<BudgetEntryFeeMap> BudgetEntryFeeMaps { get; set; }
    }
}