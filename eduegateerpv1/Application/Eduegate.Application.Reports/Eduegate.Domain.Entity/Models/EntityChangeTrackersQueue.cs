using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EntityChangeTrackersQueue
    {
        public long EntityChangeTrackerQueueIID { get; set; }
        public Nullable<long> EntityChangeTrackeID { get; set; }
        public Nullable<bool> IsReprocess { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public virtual EntityChangeTracker EntityChangeTracker { get; set; }
    }
}
