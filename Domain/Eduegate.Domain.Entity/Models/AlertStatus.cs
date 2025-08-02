using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("AlertStatuses", Schema = "notification")]
    public partial class AlertStatus
    {
        public AlertStatus()
        {
            this.NotificationAlerts = new List<NotificationAlert>();
        }

        [Key]
        public int AlertStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<NotificationAlert> NotificationAlerts { get; set; }
    }
}
