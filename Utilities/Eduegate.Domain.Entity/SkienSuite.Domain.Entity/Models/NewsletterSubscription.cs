using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class NewsletterSubscription
    {
        public long NewsletterSubscriptionID { get; set; }
        public string EmailID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }
        public byte StatusID { get; set; }
        public byte CultureID { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
