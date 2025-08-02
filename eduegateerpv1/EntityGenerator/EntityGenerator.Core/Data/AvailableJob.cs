using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AvailableJobs", Schema = "hr")]
    public partial class AvailableJob
    {
        public AvailableJob()
        {
            AvailableJobTags = new HashSet<AvailableJobTag>();
        }

        [Key]
        public long JobIID { get; set; }
        [StringLength(100)]
        public string JobTitle { get; set; }
        [StringLength(50)]
        public string TypeOfJob { get; set; }
        [StringLength(1000)]
        public string JobDescription { get; set; }
        public string JobDetails { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string Status { get; set; }
        public Guid? Id { get; set; }
        public Guid? PageId { get; set; }
        public int? DepartmentId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [InverseProperty("Job")]
        public virtual ICollection<AvailableJobTag> AvailableJobTags { get; set; }
    }
}
