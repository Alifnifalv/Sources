using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models.Notification
{
    [Table("EmailTemplates", Schema = "notification")]
    public partial class EmailTemplate2
    {
        public EmailTemplate2()
        {
            EmailTemplateParameterMaps = new HashSet<EmailTemplateParameterMap>();
        }

        [Key]
        public int EmailTemplateID { get; set; }

        [StringLength(50)]
        public string TemplateName { get; set; }

        public string Subject { get; set; }

        public string EmailTemplate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public virtual ICollection<EmailTemplateParameterMap> EmailTemplateParameterMaps { get; set; }
    }
}