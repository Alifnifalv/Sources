using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmailTemplates", Schema = "marketing")]
    public partial class EmailTemplate1
    {
        public EmailTemplate1()
        {
            EmailCampaigns = new HashSet<EmailCampaign>();
        }

        [Key]
        public long EmailTemplateIID { get; set; }
        public string Subject { get; set; }
        public string EmailContent { get; set; }
        [StringLength(2000)]
        public string Parameters { get; set; }

        [InverseProperty("EmailTemplate")]
        public virtual ICollection<EmailCampaign> EmailCampaigns { get; set; }
    }
}
