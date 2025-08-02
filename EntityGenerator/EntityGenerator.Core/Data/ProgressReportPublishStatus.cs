using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProgressReportPublishStatuses", Schema = "schools")]
    public partial class ProgressReportPublishStatus
    {
        public ProgressReportPublishStatus()
        {
            ProgressReports = new HashSet<ProgressReport>();
        }

        [Key]
        public byte ProgressReportPublishStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        [StringLength(10)]
        public string StatusCode { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("PublishStatus")]
        public virtual ICollection<ProgressReport> ProgressReports { get; set; }
    }
}
