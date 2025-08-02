using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("JobStatuses", Schema = "jobs")]
    public partial class JobStatus
    {
        public JobStatus()
        {
            JobEntryDetails = new HashSet<JobEntryDetail>();
            JobEntryHeads = new HashSet<JobEntryHead>();
        }

        [Key]
        public int JobStatusID { get; set; }
        public int? JobTypeID { get; set; }
        public int? SerNo { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [ForeignKey("JobTypeID")]
        [InverseProperty("JobStatus")]
        public virtual JobActivity JobType { get; set; }
        [InverseProperty("JobStatus")]
        public virtual ICollection<JobEntryDetail> JobEntryDetails { get; set; }
        [InverseProperty("JobStatus")]
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
