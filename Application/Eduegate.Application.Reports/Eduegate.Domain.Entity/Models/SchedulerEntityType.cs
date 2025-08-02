using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SchedulerEntityType
    {
        public SchedulerEntityType()
        {
            this.EntitySchedulers = new List<EntityScheduler>();
        }

        public int SchedulerEntityTypID { get; set; }
        public string EntityName { get; set; }
        public virtual ICollection<EntityScheduler> EntitySchedulers { get; set; }
    }
}
