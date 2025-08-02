namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.NewsletterSubscription")]
    public partial class NewsletterSubscription
    {
        public long NewsletterSubscriptionID { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailID { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(200)]
        public string IPAddress { get; set; }

        public byte StatusID { get; set; }

        public byte CultureID { get; set; }

        public virtual Culture Culture { get; set; }
    }
}
