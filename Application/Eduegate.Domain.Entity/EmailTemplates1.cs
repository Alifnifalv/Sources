namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.EmailTemplates")]
    public partial class EmailTemplates1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmailTemplates1()
        {
            EmailCampaigns = new HashSet<EmailCampaign>();
        }

        [Key]
        public long EmailTemplateIID { get; set; }

        public string Subject { get; set; }

        public string EmailContent { get; set; }

        [StringLength(2000)]
        public string Parameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmailCampaign> EmailCampaigns { get; set; }
    }
}
