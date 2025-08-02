using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BudgetEntryCostCenterMaps", Schema = "budget")]
    public partial class BudgetEntryCostCenterMap
    {
        [Key]
        public long BudgetEntryCostCenterMapIID { get; set; }
        public long? BudgetEntryID { get; set; }
        public int? CostCenterID { get; set; }

        [ForeignKey("BudgetEntryID")]
        [InverseProperty("BudgetEntryCostCenterMaps")]
        public virtual BudgetEntry BudgetEntry { get; set; }
        [ForeignKey("CostCenterID")]
        [InverseProperty("BudgetEntryCostCenterMaps")]
        public virtual CostCenter CostCenter { get; set; }
    }
}
