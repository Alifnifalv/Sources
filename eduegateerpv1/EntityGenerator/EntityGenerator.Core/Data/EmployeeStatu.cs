using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeStatus", Schema = "payroll")]
    public partial class EmployeeStatu
    {
        public EmployeeStatu()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public byte StatusID { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [InverseProperty("StatusNavigation")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
