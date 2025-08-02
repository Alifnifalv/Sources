namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hr.AvailableJobs")]
    public partial class AvailableJob
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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
        public string Status { get; set; }

        public Guid? Id { get; set; }

        public Guid? PageId { get; set; }

        public int? DepartmentId { get; set; }

        public DateTime? CreatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AvailableJobTag> AvailableJobTags { get; set; }
    }
}
