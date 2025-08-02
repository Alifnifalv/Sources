using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? SuggestedValue { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Percentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? EstimateValue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SuggestedStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SuggestedEndDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }

        [ForeignKey("BudgetID")]
        [InverseProperty("BudgetEntries")]
        public virtual Budget1 Budget { get; set; }
        [ForeignKey("BudgetSuggestionID")]
        [InverseProperty("BudgetEntries")]
        public virtual BudgetSuggestion BudgetSuggestion { get; set; }
        [InverseProperty("BudgetEntry")]
        public virtual ICollection<BudgetEntryAccountMap> BudgetEntryAccountMaps { get; set; }
        [InverseProperty("BudgetEntry")]
        public virtual ICollection<BudgetEntryAllocation> BudgetEntryAllocations { get; set; }
        [InverseProperty("BudgetEntry")]
        public virtual ICollection<BudgetEntryCostCenterMap> BudgetEntryCostCenterMaps { get; set; }
        [InverseProperty("BudgetEntry")]
        public virtual ICollection<BudgetEntryFeeMap> BudgetEntryFeeMaps { get; set; }
    }
}
