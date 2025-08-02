namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("notification.NotificationsQueue")]
    public partial class NotificationsQueue
    {
        [Key]
        public long NotificationQueueIID { get; set; }

        public int NotificationTypeID { get; set; }

        public bool? IsReprocess { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual NotificationType NotificationType { get; set; }
    }
}
