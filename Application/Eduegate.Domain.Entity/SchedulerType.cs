namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schedulers.SchedulerTypes")]
    public partial class SchedulerType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchedulerType()
        {
            EntitySchedulers = new HashSet<EntityScheduler>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SchedulerTypeID { get; set; }

        [StringLength(50)]
        public string SchedulerTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntityScheduler> EntitySchedulers { get; set; }
    }
}
