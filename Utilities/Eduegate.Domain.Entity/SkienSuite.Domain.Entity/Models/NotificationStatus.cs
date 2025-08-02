using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class NotificationStatus
    {
        public NotificationStatus()
        {
            this.NotificationLogs = new List<NotificationLog>();
        }

        public int NotificationStatusID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<NotificationLog> NotificationLogs { get; set; }
    }
}
