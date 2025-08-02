using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("NotificationsQueue", Schema = "notification")]
    public partial class NotificationsQueue
    {
        [Key]
        public long NotificationQueueIID { get; set; }
        public int NotificationTypeID { get; set; }
        public bool? IsReprocess { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("NotificationTypeID")]
        [InverseProperty("NotificationsQueues")]
        public virtual NotificationType NotificationType { get; set; }
    }
}
