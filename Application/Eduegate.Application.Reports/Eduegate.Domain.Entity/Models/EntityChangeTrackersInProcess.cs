using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EntityChangeTrackersInProcess
    {
        public long EntityChangeTrackerInProcessIID { get; set; }
        public Nullable<long> EntityChangeTrackerID { get; set; }
        public Nullable<bool> IsReprocess { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual EntityChangeTracker EntityChangeTracker { get; set; }
    }
}
