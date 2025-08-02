namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jobs.JobStatuses")]
    public partial class JobStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobStatus()
        {
            JobEntryDetails = new HashSet<JobEntryDetail>();
            JobEntryHeads = new HashSet<JobEntryHead>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int JobStatusID { get; set; }

        public int? JobTypeID { get; set; }

        public int? SerNo { get; set; }

        [StringLength(50)]
        public string StatusName { get; set; }

        public virtual JobActivity JobActivity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobEntryDetail> JobEntryDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
