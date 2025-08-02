using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class AlertStatus
    {
        public AlertStatus()
        {
            this.NotificationAlerts = new List<NotificationAlert>();
        }

        public int AlertStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<NotificationAlert> NotificationAlerts { get; set; }
    }
}
