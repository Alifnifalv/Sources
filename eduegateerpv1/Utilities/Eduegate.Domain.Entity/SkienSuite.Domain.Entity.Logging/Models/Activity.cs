using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Logging.Models
{
    public partial class Activity
    {
        public long ActivitiyIID { get; set; }
        public Nullable<int> ActivityTypeID { get; set; }
        public string Description { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public virtual ActivityType ActivityType { get; set; }
    }
}
