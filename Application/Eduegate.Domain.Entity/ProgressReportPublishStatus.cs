namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ProgressReportPublishStatuses")]
    public partial class ProgressReportPublishStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProgressReportPublishStatus()
        {
            ProgressReports = new HashSet<ProgressReport>();
        }

        public byte ProgressReportPublishStatusID { get; set; }

        [StringLength(50)]
        public string StatusName { get; set; }

        [StringLength(10)]
        public string StatusCode { get; set; }

        public bool? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProgressReport> ProgressReports { get; set; }
    }
}
