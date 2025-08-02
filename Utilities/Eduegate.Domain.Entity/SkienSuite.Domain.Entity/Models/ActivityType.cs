using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ActivityType
    {
        public ActivityType()
        {
            this.Activities = new List<Activity>();
        }

        public int ActivityTypeID { get; set; }
        public string ActivityTypeName { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
