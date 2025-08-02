using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class NewsletterJob
    {
        public NewsletterJob()
        {
            this.NewsLetterJobEmails = new List<NewsLetterJobEmail>();
            this.NewsLetterLoggedMessages = new List<NewsLetterLoggedMessage>();
        }

        public int JobID { get; set; }
        public int TemplateID { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string PlainText1 { get; set; }
        public string PlainText2 { get; set; }
        public string PlainText3 { get; set; }
        public string PlainText4 { get; set; }
        public string EmailIds { get; set; }
        public Nullable<bool> NewsSubscribers { get; set; }
        public Nullable<bool> Customers { get; set; }
        public Nullable<System.DateTime> createdate { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> CompletedOn { get; set; }
        public int SubscriberCount { get; set; }
        public int EmailsSent { get; set; }
        public virtual ICollection<NewsLetterJobEmail> NewsLetterJobEmails { get; set; }
        public virtual NewsLetterTemplate NewsLetterTemplate { get; set; }
        public virtual ICollection<NewsLetterLoggedMessage> NewsLetterLoggedMessages { get; set; }
    }
}
