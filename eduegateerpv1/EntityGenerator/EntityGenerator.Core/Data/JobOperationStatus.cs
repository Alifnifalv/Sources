using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("JobOperationStatuses", Schema = "jobs")]
    public partial class JobOperationStatus
    {
        public JobOperationStatus()
        {
            JobEntryHeads = new HashSet<JobEntryHead>();
        }

        [Key]
        public byte JobOperationStatusID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("JobOperationStatus")]
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
