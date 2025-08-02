using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BudgetEntryAccountMaps", Schema = "budget")]
    public partial class BudgetEntryAccountMap
    {
        [Key]
        public long BudgetEntryAccountMapIID { get; set; }
        public long? BudgetEntryID { get; set; }
        public int? GroupID { get; set; }
        public long? AccountID { get; set; }
        public int? CostCenterID { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("BudgetEntryAccountMaps")]
        public virtual Account Account { get; set; }
        [ForeignKey("BudgetEntryID")]
        [InverseProperty("BudgetEntryAccountMaps")]
        public virtual BudgetEntry BudgetEntry { get; set; }
        [ForeignKey("CostCenterID")]
        [InverseProperty("BudgetEntryAccountMaps")]
        public virtual CostCenter CostCenter { get; set; }
        [ForeignKey("GroupID")]
        [InverseProperty("BudgetEntryAccountMaps")]
        public virtual Group Group { get; set; }
    }
}
