using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("SMSNotificationDatas", Schema = "notification")]
    public partial class SMSNotificationData
    {
        public long SMSNotificationDataIID { get; set; }
        public long NotificationQueueID { get; set; }
        public int SMSNotificationTypeID { get; set; }
        public string SMSContent { get; set; }
        [StringLength(50)]
        public string TemplateName { get; set; }
        [StringLength(500)]
        public string Subject { get; set; }
        [StringLength(1000)]
        public string FromMobileNumber { get; set; }
        [StringLength(1000)]
        public string ToMobileNumber { get; set; }
        public string SerializedJsonParameters { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public long? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("SMSNotificationTypeID")]
        public virtual SMSNotificationType SMSNotificationType { get; set; }
    }
}
