using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class NotificationView
    {
        public long NotificationQueueID { get; set; }
        public string Name { get; set; }
        public string NotificationStatusName { get; set; }
        public string ToEmailID { get; set; }
        public string FromEmailID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
