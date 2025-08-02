using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("NewsletterSubscription", Schema = "cms")]
    public partial class NewsletterSubscription
    {
        [Key]
        public long NewsletterSubscriptionID { get; set; }
        public string EmailID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }
        public byte StatusID { get; set; }
        public byte CultureID { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
