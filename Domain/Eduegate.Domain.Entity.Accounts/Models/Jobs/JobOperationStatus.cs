using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Jobs
{
    [Table("JobOperationStatuses", Schema = "jobs")]
    public partial class JobOperationStatus
    {
        public JobOperationStatus()
        {
            //AvailableJobs = new HashSet<AvailableJob>();
            JobEntryHeads = new HashSet<JobEntryHead>();
        }

        [Key]
        public byte JobOperationStatusID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        //public virtual ICollection<AvailableJob> AvailableJobs { get; set; }

        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}