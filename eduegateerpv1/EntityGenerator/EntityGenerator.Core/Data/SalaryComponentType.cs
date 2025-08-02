using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalaryComponentTypes", Schema = "payroll")]
    public partial class SalaryComponentType
    {
        public SalaryComponentType()
        {
            SalaryComponents = new HashSet<SalaryComponent>();
        }

        [Key]
        public byte SalaryComponentTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }

        [InverseProperty("ComponentType")]
        public virtual ICollection<SalaryComponent> SalaryComponents { get; set; }
    }
}
