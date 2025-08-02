using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MaritalStatus
    {
        public MaritalStatus()
        {
            this.Employees = new List<Employee>();
        }

        public int MaritalStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
