using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalaryMethod", Schema = "payroll")]
    public partial class SalaryMethod
    {
        public SalaryMethod()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int SalaryMethodID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string SalaryMethodName { get; set; }

        [InverseProperty("SalaryMethod")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
