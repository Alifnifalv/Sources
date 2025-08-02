namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sync.TrackerStatuses")]
    public partial class TrackerStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrackerStatus()
        {
            EntityChangeTrackers = new HashSet<EntityChangeTracker>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TrackerStatusID { get; set; }

        [StringLength(50)]
        public string StatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntityChangeTracker> EntityChangeTrackers { get; set; }
    }
}
