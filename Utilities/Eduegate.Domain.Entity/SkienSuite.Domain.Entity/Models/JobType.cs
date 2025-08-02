using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class JobType
    {
        public JobType()
        {
            this.Employees = new List<Employee>();
        }

        public int JobTypeID { get; set; }
        public string JobTypeName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
