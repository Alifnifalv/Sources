using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("JobStatuses", Schema = "jobs")]
    public partial class JobStatus
    {
        public JobStatus()
        {
            this.JobEntryDetails = new List<JobEntryDetail>();
            this.JobEntryHeads = new List<JobEntryHead>();
        }

        [Key]
        public int JobStatusID { get; set; }
        public Nullable<int> JobTypeID { get; set; }
        public Nullable<int> SerNo { get; set; }
        public string StatusName { get; set; }
        public virtual JobActivity JobType { get; set; } 

        //public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual ICollection<JobEntryDetail> JobEntryDetails { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
