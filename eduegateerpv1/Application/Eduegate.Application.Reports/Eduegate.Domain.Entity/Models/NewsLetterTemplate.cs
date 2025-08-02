using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class NewsLetterTemplate
    {
        public NewsLetterTemplate()
        {
            this.NewsletterJobs = new List<NewsletterJob>();
        }

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
        public virtual ICollection<NewsletterJob> NewsletterJobs { get; set; }
    }
}
