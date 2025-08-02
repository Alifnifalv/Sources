using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Jobs
{
    [Table("Priorities", Schema = "jobs")]
    public partial class Priority
    {
        public Priority()
        {
            JobEntryHeads = new HashSet<JobEntryHead>();
        }

        [Key]
        public byte PriorityID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}