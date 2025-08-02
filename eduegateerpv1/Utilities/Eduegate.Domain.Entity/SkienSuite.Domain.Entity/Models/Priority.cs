using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Priority
    {
        public Priority()
        {
            this.JobEntryHeads = new List<JobEntryHead>();
        }

        public byte PriorityID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
