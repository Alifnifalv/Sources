using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BudgetTypes", Schema = "budget")]
    public partial class BudgetType
    {
        public BudgetType()
        {
            Budget1 = new HashSet<Budget1>();
        }

        [Key]
        public byte BudgetTypeID { get; set; }
        [StringLength(50)]
        public string BudgetTypeName { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("BudgetType")]
        public virtual ICollection<Budget1> Budget1 { get; set; }
    }
}
