using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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

        [InverseProperty("JobSize")]
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
