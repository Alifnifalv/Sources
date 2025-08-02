using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("EmailNotificationData", Schema = "notification")]
    public partial class EmailNotificationData
    {
        [Key]
        public long EmailMetaDataIID { get; set; }
        public long NotificationQueueID { get; set; }
        public int EmailNotificationTypeID { get; set; }
        public string EmailData { get; set; }
        public string TemplateName { get; set; }
        public string Subject { get; set; }
        public string FromEmailID { get; set; }
        public string ToEmailID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual EmailNotificationType EmailNotificationType { get; set; }
        public string SerializedJsonParameters { get; set; }
    }
}
