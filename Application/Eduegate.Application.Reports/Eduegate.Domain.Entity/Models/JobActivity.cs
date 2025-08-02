using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class JobActivity
    {
        public JobActivity()
        {
            this.JobStatuses = new List<JobStatus>();
        }

        public int JobActivityID { get; set; }
        public string ActivityName { get; set; }
        public virtual ICollection<JobStatus> JobStatuses { get; set; }
    }
}
