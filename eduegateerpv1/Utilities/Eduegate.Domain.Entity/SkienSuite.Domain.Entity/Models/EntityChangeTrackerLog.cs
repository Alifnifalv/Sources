using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EntityChangeTrackerLog
    {
        public long EntityChangeTrackerLogID { get; set; }
        public long EntityChangeTrackerType { get; set; }
        public long EntityChangeTrackerTypeID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> SyncedOn { get; set; }
    }
}
