using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Jobs
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

        public virtual JobActivity JobType { get; set; }

        public virtual ICollection<JobEntryDetail> JobEntryDetails { get; set; }

        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}