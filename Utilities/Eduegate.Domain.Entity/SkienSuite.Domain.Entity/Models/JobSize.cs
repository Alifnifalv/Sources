using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class JobSize
    {
        public JobSize()
        {
            this.JobEntryHeads = new List<JobEntryHead>();
        }

        public short JobSizeID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
