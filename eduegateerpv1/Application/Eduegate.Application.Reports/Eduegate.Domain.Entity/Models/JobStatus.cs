using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class JobStatus
    {
        public JobStatus()
        {
            this.JobEntryDetails = new List<JobEntryDetail>();
            this.JobEntryHeads = new List<JobEntryHead>();
        }

        public int JobStatusID { get; set; }
        public Nullable<int> JobTypeID { get; set; }
        public Nullable<int> SerNo { get; set; }
        public string StatusName { get; set; }
        public virtual JobActivity JobActivity { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual ICollection<JobEntryDetail> JobEntryDetails { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
