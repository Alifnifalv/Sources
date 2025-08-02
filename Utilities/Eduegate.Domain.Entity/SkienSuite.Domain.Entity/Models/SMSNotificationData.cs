using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SMSNotificationData
    {
        public long SMSNotificationDataIID { get; set; }
        public long NotificationQueueID { get; set; }
        public int SMSNotificationTypeID { get; set; }
        public string SMSContent { get; set; }
        public string TemplateName { get; set; }
        public string Subject { get; set; }
        public string FromMobileNumber { get; set; }
        public string ToMobileNumber { get; set; }
        public string SerializedJsonParameters { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual SMSNotificationType SMSNotificationType { get; set; }
    }
}
