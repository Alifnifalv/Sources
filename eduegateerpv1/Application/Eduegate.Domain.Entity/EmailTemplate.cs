namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("crm.EmailTemplates")]
    public partial class EmailTemplate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmailTemplate()
        {
            Communications = new HashSet<Communication>();
            LeadEmailMaps = new HashSet<LeadEmailMap>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmailTemplateID { get; set; }

        [StringLength(50)]
        public string TemplateName { get; set; }

        public string Template { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Communication> Communications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeadEmailMap> LeadEmailMaps { get; set; }
    }
}
