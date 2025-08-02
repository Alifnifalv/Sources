using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class JobOperationStatus
    {
        public JobOperationStatus()
        {
            this.JobEntryHeads = new List<JobEntryHead>();
        }

        public byte JobOperationStatusID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
