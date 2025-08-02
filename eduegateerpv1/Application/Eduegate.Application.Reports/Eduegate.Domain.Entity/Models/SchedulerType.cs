using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SchedulerType
    {
        public SchedulerType()
        {
            this.EntitySchedulers = new List<EntityScheduler>();
        }

        public int SchedulerTypeID { get; set; }
        public string SchedulerTypeName { get; set; }
        public virtual ICollection<EntityScheduler> EntitySchedulers { get; set; }
    }
}
