using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Priorities", Schema = "jobs")]
    public partial class Priority
    {
        public Priority()
        {
            this.JobEntryHeads = new List<JobEntryHead>();
        }

        [Key]
        public byte PriorityID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
