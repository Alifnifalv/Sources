using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class NewsLetterLoggedMessage
    {
        [Key]
        public int LoggedMessageID { get; set; }
        public int JobID { get; set; }
        public System.DateTime DateTime { get; set; }
        public string Message { get; set; }
        public virtual NewsletterJob NewsletterJob { get; set; }
    }
}
