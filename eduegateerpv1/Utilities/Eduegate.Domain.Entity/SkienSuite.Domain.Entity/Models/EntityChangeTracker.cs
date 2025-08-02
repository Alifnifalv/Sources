using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EntityChangeTracker
    {
        public EntityChangeTracker()
        {
            this.EntityChangeTrackersQueues = new List<EntityChangeTrackersQueue>();
            this.EntityChangeTrackersInProcesses = new List<EntityChangeTrackersInProcess>();
        }

        public long EntityChangeTrackerIID { get; set; }
        public Nullable<int> EntityID { get; set; }
        public Nullable<int> OperationTypeID { get; set; }
        public Nullable<long> ProcessedID { get; set; }
        public string ProcessedFields { get; set; }
        public Nullable<int> TrackerStatusID { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public string BatchNo { get; set; }
        public virtual SyncEntity SyncEntity { get; set; }
        public virtual OperationType OperationType { get; set; }
        public virtual TrackerStatus TrackerStatus { get; set; }
        public virtual ICollection<EntityChangeTrackersQueue> EntityChangeTrackersQueues { get; set; }
        public virtual ICollection<EntityChangeTrackersInProcess> EntityChangeTrackersInProcesses { get; set; }
    }
}
