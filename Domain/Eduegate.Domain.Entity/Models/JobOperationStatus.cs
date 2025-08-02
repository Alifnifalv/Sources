using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("JobOperationStatuses", Schema = "jobs")]
    public partial class JobOperationStatus
    {
        public JobOperationStatus()
        {
            this.JobEntryHeads = new List<JobEntryHead>();
            AvailableJobs = new HashSet<AvailableJob>();
        }

        [Key]
        public byte JobOperationStatusID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }

        public virtual ICollection<AvailableJob> AvailableJobs { get; set; }
    }
}
