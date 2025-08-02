using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class NewsletterSubscriber
    {
        public int SubscriberID { get; set; }
        public string EmailID { get; set; }
        public System.DateTime RegisteredOn { get; set; }
        public string IPAddress { get; set; }
        public string IPCountry { get; set; }
        public string Status { get; set; }
        public string Lang { get; set; }
    }
}
