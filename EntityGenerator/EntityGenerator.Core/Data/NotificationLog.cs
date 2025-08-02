using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("NotificationLogs", Schema = "notification")]
    public partial class NotificationLog
    {
        [Key]
        public long NotificationLogsIID { get; set; }
        public int NotificationTypeID { get; set; }
        public long NotificationQueueID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [StringLength(100)]
        public string Result { get; set; }
        [StringLength(500)]
        public string Reason { get; set; }
        public int NotificationStatusID { get; set; }

        [ForeignKey("NotificationStatusID")]
        [InverseProperty("NotificationLogs")]
        public virtual NotificationStatus NotificationStatus { get; set; }
        [ForeignKey("NotificationTypeID")]
        [InverseProperty("NotificationLogs")]
        public virtual NotificationType NotificationType { get; set; }
    }
}
