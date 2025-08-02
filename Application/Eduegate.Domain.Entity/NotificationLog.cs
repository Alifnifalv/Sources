namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("notification.NotificationLogs")]
    public partial class NotificationLog
    {
        [Key]
        public long NotificationLogsIID { get; set; }

        public int NotificationTypeID { get; set; }

        public long NotificationQueueID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(100)]
        public string Result { get; set; }

        [StringLength(500)]
        public string Reason { get; set; }

        public int NotificationStatusID { get; set; }

        public virtual NotificationStatus NotificationStatus { get; set; }

        public virtual NotificationType NotificationType { get; set; }
    }
}
