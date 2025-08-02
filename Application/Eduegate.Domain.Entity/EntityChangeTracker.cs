namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sync.EntityChangeTracker")]
    public partial class EntityChangeTracker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EntityChangeTracker()
        {
            EntityChangeTrackersQueues = new HashSet<EntityChangeTrackersQueue>();
            EntityChangeTrackersInProcesses = new HashSet<EntityChangeTrackersInProcess>();
        }

        [Key]
        public long EntityChangeTrackerIID { get; set; }

        public int? EntityID { get; set; }

        public int? OperationTypeID { get; set; }

        public long? ProcessedID { get; set; }

        [StringLength(1000)]
        public string ProcessedFields { get; set; }

        public int? TrackerStatusID { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [StringLength(100)]
        public string BatchNo { get; set; }

        public virtual SyncEntity SyncEntity { get; set; }

        public virtual OperationType OperationType { get; set; }

        public virtual TrackerStatus TrackerStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntityChangeTrackersQueue> EntityChangeTrackersQueues { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntityChangeTrackersInProcess> EntityChangeTrackersInProcesses { get; set; }
    }
}
