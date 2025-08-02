using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class NotificationType
    {
        public NotificationType()
        {
            this.NotificationLogs = new List<NotificationLog>();
            this.NotificationsProcessings = new List<NotificationsProcessing>();
            this.NotificationsQueues = new List<NotificationsQueue>();
        }

        public int NotificationTypeID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<NotificationLog> NotificationLogs { get; set; }
        public virtual ICollection<NotificationsProcessing> NotificationsProcessings { get; set; }
        public virtual ICollection<NotificationsQueue> NotificationsQueues { get; set; }
    }
}
