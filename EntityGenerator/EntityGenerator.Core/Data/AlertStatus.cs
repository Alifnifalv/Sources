using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AlertStatuses", Schema = "notification")]
    public partial class AlertStatus
    {
        public AlertStatus()
        {
            NotificationAlerts = new HashSet<NotificationAlert>();
        }

        [Key]
        public int AlertStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("AlertStatus")]
        public virtual ICollection<NotificationAlert> NotificationAlerts { get; set; }
    }
}
