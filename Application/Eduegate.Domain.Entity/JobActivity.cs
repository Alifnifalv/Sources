namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jobs.JobActivities")]
    public partial class JobActivity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobActivity()
        {
            JobEntryHeads = new HashSet<JobEntryHead>();
            JobStatuses = new HashSet<JobStatus>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int JobActivityID { get; set; }

        [StringLength(50)]
        public string ActivityName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobStatus> JobStatuses { get; set; }
    }
}
