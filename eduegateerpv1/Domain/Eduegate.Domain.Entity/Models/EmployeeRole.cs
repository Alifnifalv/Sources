using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("EmployeeRoles", Schema = "payroll")]
    public partial class EmployeeRole
    {
        public EmployeeRole()
        {
            this.EmployeeRoleMaps = new List<EmployeeRoleMap>();
            this.Employees = new List<Employee>();
        }

        [Key]
        public int EmployeeRoleID { get; set; }
        public string EmployeeRoleName { get; set; }
        public virtual ICollection<EmployeeRoleMap> EmployeeRoleMaps { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
