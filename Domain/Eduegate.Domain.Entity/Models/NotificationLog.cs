using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("NotificationLogs", Schema = "notification")]
    public partial class NotificationLog
    {
        [Key]
        public long NotificationLogsIID { get; set; }
        public int NotificationTypeID { get; set; }
        public long NotificationQueueID { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Result { get; set; }
        public string Reason { get; set; }
        public int NotificationStatusID { get; set; }
        public virtual NotificationType NotificationType { get; set; }
    }
}
