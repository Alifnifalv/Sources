using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SyncEntity
    {
        public SyncEntity()
        {
            this.EntityChangeTrackers = new List<EntityChangeTracker>();
        }

        public int EntityID { get; set; }
        public string EntityName { get; set; }
        public string EntityDataSource { get; set; }
        public virtual ICollection<EntityChangeTracker> EntityChangeTrackers { get; set; }
    }
}
