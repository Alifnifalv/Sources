using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TrackerStatus
    {
        public TrackerStatus()
        {
            this.EntityChangeTrackers = new List<EntityChangeTracker>();
        }

        public int TrackerStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<EntityChangeTracker> EntityChangeTrackers { get; set; }
    }
}
