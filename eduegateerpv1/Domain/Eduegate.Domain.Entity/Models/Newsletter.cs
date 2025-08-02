using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Newsletter
    {
        [Key]
        public long NewsletterID { get; set; }
        public string EmailAddress { get; set; }
        public System.DateTime SubscribedOn { get; set; }
        public bool Newsletter1 { get; set; }
        public Nullable<int> EmailSend { get; set; }
        public Nullable<int> EmailBounced { get; set; }
    }
}
