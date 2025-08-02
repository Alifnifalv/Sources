using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class NewsLetterJobEmail
    {
        public int JobID { get; set; }
        public string SubscriberId { get; set; }
        public int SubscriberType { get; set; }
        public bool MailSend { get; set; }
        public Nullable<System.DateTime> SendTime { get; set; }
        public virtual NewsletterJob NewsletterJob { get; set; }
    }
}
