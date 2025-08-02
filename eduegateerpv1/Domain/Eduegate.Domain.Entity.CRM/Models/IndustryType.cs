using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.CRM.Models
{
    [Table("IndustryTypes", Schema = "crm")]
    public partial class IndustryType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IndustryType()
        {
            Leads = new HashSet<Lead>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IndustryTypeID { get; set; }

        [StringLength(50)]
        public string IndustryTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lead> Leads { get; set; }
    }
}