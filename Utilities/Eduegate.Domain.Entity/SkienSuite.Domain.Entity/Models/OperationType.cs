using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OperationType
    {
        public OperationType()
        {
            this.EntityChangeTrackers = new List<EntityChangeTracker>();
        }

        public int OperationTypeID { get; set; }
        public string OperationName { get; set; }
        public virtual ICollection<EntityChangeTracker> EntityChangeTrackers { get; set; }
    }
}
