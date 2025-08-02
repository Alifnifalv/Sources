using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BudgetEntryFeeMaps", Schema = "budget")]
    public partial class BudgetEntryFeeMap
    {
        [Key]
        public long BudgetEntryFeeMapIID { get; set; }
        public long? BudgetEntryID { get; set; }
        public int? FeeMasterID { get; set; }

        [ForeignKey("BudgetEntryID")]
        [InverseProperty("BudgetEntryFeeMaps")]
        public virtual BudgetEntry BudgetEntry { get; set; }
        [ForeignKey("FeeMasterID")]
        [InverseProperty("BudgetEntryFeeMaps")]
        public virtual FeeMaster FeeMaster { get; set; }
    }
}
