using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class NotificationsQueue
    {
        public long NotificationQueueIID { get; set; }
        public int NotificationTypeID { get; set; }
        public Nullable<bool> IsReprocess { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public virtual NotificationType NotificationType { get; set; }
    }
}
