using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EmployeeRole
    {
        public EmployeeRole()
        {
            this.EmployeeRoleMaps = new List<EmployeeRoleMap>();
            this.Employees = new List<Employee>();
        }

        public int EmployeeRoleID { get; set; }
        public string EmployeeRoleName { get; set; }
        public virtual ICollection<EmployeeRoleMap> EmployeeRoleMaps { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
