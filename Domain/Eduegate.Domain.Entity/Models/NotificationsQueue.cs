using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("NotificationsQueue", Schema = "notification")]
    public partial class NotificationsQueue
    {
        [Key]
        public long NotificationQueueIID { get; set; }
        public int NotificationTypeID { get; set; }
        public Nullable<bool> IsReprocess { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public virtual NotificationType NotificationType { get; set; }
    }
}
