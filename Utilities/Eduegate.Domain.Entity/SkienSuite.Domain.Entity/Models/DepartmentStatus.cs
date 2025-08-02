using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DepartmentStatus
    {
        public DepartmentStatus()
        {
            this.Departments = new List<Department>();
        }

        public byte DepartmentStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
