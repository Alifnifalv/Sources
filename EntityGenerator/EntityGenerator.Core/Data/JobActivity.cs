using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("JobActivities", Schema = "jobs")]
    public partial class JobActivity
    {
        public JobActivity()
        {
            JobEntryHeads = new HashSet<JobEntryHead>();
            JobStatus = new HashSet<JobStatus>();
        }

        [Key]
        public int JobActivityID { get; set; }
        [StringLength(50)]
        public string ActivityName { get; set; }

        [InverseProperty("JobActivity")]
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
        [InverseProperty("JobType")]
        public virtual ICollection<JobStatus> JobStatus { get; set; }
    }
}
