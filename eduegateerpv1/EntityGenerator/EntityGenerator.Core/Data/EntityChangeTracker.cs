using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EntityChangeTracker", Schema = "sync")]
    public partial class EntityChangeTracker
    {
        public EntityChangeTracker()
        {
            EntityChangeTrackersInProcesses = new HashSet<EntityChangeTrackersInProcess>();
            EntityChangeTrackersQueues = new HashSet<EntityChangeTrackersQueue>();
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string BatchNo { get; set; }

        [ForeignKey("EntityID")]
        [InverseProperty("EntityChangeTrackers")]
        public virtual SyncEntity Entity { get; set; }
        [ForeignKey("OperationTypeID")]
        [InverseProperty("EntityChangeTrackers")]
        public virtual OperationType OperationType { get; set; }
        [ForeignKey("TrackerStatusID")]
        [InverseProperty("EntityChangeTrackers")]
        public virtual TrackerStatus TrackerStatus { get; set; }
        [InverseProperty("EntityChangeTracker")]
        public virtual ICollection<EntityChangeTrackersInProcess> EntityChangeTrackersInProcesses { get; set; }
        [InverseProperty("EntityChangeTracke")]
        public virtual ICollection<EntityChangeTrackersQueue> EntityChangeTrackersQueues { get; set; }
    }
}
