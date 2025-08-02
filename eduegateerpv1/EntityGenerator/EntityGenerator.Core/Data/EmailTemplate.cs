using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmailTemplates", Schema = "crm")]
    public partial class EmailTemplate
    {
        public EmailTemplate()
        {
            Communications = new HashSet<Communication>();
            LeadEmailMaps = new HashSet<LeadEmailMap>();
        }

        [Key]
        public int EmailTemplateID { get; set; }
        [StringLength(50)]
        public string TemplateName { get; set; }
        public string Template { get; set; }

        [InverseProperty("EmailTemplate")]
        public virtual ICollection<Communication> Communications { get; set; }
        [InverseProperty("EmailTemplate")]
        public virtual ICollection<LeadEmailMap> LeadEmailMaps { get; set; }
    }
}
