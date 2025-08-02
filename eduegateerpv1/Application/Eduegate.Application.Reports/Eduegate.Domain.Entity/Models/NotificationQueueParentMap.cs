using System;

namespace Eduegate.Domain.Entity.Models
{
    public class NotificationQueueParentMap
    {
        public long NotificationQueueParentMapIID { get; set; }
        public long NotificationQueueID { get; set; }
        public long ParentNotificationQueueID { get; set; }
        public Nullable<bool> IsNotificationSuccess { get; set; }
    }
}
