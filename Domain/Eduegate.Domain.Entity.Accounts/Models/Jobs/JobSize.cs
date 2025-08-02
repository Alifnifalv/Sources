using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Jobs
{
    [Table("JobSizes", Schema = "jobs")]
    public partial class JobSize
    {
        public JobSize()
        {
            JobEntryHeads = new HashSet<JobEntryHead>();
        }

        [Key]
        public short JobSizeID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}