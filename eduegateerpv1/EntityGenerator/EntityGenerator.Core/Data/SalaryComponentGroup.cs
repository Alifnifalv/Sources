using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalaryComponentGroup", Schema = "payroll")]
    public partial class SalaryComponentGroup
    {
        public SalaryComponentGroup()
        {
            SalaryComponents = new HashSet<SalaryComponent>();
        }

        [Key]
        public byte SalaryComponentGroupID { get; set; }
        [StringLength(50)]
        public string SalaryComponentGroupName { get; set; }

        [InverseProperty("SalaryComponentGroup")]
        public virtual ICollection<SalaryComponent> SalaryComponents { get; set; }
    }
}
