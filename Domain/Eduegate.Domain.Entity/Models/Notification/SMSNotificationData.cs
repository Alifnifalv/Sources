using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models.Notification
{
    public partial class SMSNotificationData
    {
        [Key]
        public long SMSNotificationDataIID { get; set; }
        public long NotificationQueueID { get; set; }
        public int SMSNotificationTypeID { get; set; }
        public string SMSContent { get; set; }
        public string TemplateName { get; set; }
        public string Subject { get; set; }
        public string FromMobileNumber { get; set; }
        public string ToMobileNumber { get; set; }
        public string SerializedJsonParameters { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public long? CreatedBy { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual SMSNotificationType SMSNotificationType { get; set; }
    }
}
