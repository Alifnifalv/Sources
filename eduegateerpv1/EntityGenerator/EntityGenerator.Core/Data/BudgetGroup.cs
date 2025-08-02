using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BudgetGroups", Schema = "budget")]
    public partial class BudgetGroup
    {
        public BudgetGroup()
        {
            Budget1 = new HashSet<Budget1>();
        }

        [Key]
        public byte BudgetGroupID { get; set; }
        [StringLength(50)]
        public string BudgetGroupName { get; set; }

        [InverseProperty("BudgetGroup")]
        public virtual ICollection<Budget1> Budget1 { get; set; }
    }
}
