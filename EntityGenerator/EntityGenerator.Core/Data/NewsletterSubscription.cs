using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("NewsletterSubscription", Schema = "cms")]
    public partial class NewsletterSubscription
    {
        [Key]
        public long NewsletterSubscriptionID { get; set; }
        [Required]
        [StringLength(100)]
        public string EmailID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(200)]
        public string IPAddress { get; set; }
        public byte StatusID { get; set; }
        public byte CultureID { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("NewsletterSubscriptions")]
        public virtual Culture Culture { get; set; }
    }
}
