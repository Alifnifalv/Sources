using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SMSNotificationType
    {
        public SMSNotificationType()
        {
            this.SMSNotificationDatas = new List<SMSNotificationData>();
        }

        public int SMSNotificationTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TemplateFilePath { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public byte[] TimeStamp { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Subject { get; set; }
        public string ToCC { get; set; }
        public string ToBCC { get; set; }
        public virtual ICollection<SMSNotificationData> SMSNotificationDatas { get; set; }
    }
}
