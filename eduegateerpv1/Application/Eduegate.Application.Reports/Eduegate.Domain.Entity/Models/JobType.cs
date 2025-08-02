using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class JobType
    {
        public JobType()
        {
            this.JobStatuses = new List<JobStatus>();
        }

        public int JobTypeID { get; set; }
        public string JobTypeName { get; set; }
        public virtual ICollection<JobStatus> JobStatuses { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
