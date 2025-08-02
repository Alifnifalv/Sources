using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class NotificationLog
    {
        public long NotificationLogsIID { get; set; }
        public int NotificationTypeID { get; set; }
        public long NotificationQueueID { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Result { get; set; }
        public string Reason { get; set; }
        public int NotificationStatusID { get; set; }
        public virtual NotificationStatus NotificationStatus { get; set; }
        public virtual NotificationType NotificationType { get; set; }
    }
}
