using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Community.Models
{
    [Table("EducationTypes", Schema = "communities")]
    public partial class EducationType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EducationType()
        {
            EducationDetails = new HashSet<EducationDetail>();
        }
        [Key]
        public byte EducationTypeID { get; set; }

        [StringLength(500)]
        public string EducationDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EducationDetail> EducationDetails { get; set; }
    }
}