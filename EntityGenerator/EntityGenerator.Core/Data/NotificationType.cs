using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("NotificationTypes", Schema = "notification")]
    public partial class NotificationType
    {
        public NotificationType()
        {
            NotificationLogs = new HashSet<NotificationLog>();
            NotificationsProcessings = new HashSet<NotificationsProcessing>();
            NotificationsQueues = new HashSet<NotificationsQueue>();
        }

        [Key]
        public int NotificationTypeID { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [InverseProperty("NotificationType")]
        public virtual ICollection<NotificationLog> NotificationLogs { get; set; }
        [InverseProperty("NotificationType")]
        public virtual ICollection<NotificationsProcessing> NotificationsProcessings { get; set; }
        [InverseProperty("NotificationType")]
        public virtual ICollection<NotificationsQueue> NotificationsQueues { get; set; }
    }
}
