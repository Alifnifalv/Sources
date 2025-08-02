using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.CRM.Models
{
    [Table("OpportunityTypes", Schema = "crm")]
    public partial class OpportunityType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OpportunityType()
        {
            Opportunities = new HashSet<Opportunity>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OpportunityTypeID { get; set; }

        [StringLength(50)]
        public string OpportunityTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opportunity> Opportunities { get; set; }
    }
}