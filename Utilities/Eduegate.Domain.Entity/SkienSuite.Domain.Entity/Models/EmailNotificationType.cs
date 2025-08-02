using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EmailNotificationType
    {
        public EmailNotificationType()
        {
            this.EmailNotificationDatas = new List<EmailNotificationData>();
        }

        public int EmailNotificationTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmailTemplateFilePath { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public byte[] TimeStamp { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string EmailSubject { get; set; }
        public string ToCCEmailID { get; set; }
        public string ToBCCEmailID { get; set; }
        public virtual ICollection<EmailNotificationData> EmailNotificationDatas { get; set; }
    }
}
