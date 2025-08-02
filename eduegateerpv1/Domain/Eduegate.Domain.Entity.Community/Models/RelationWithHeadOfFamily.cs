using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Community.Models
{
    [Table("RelationWithHeadOfFamilies", Schema = "communities")]
    public partial class RelationWithHeadOfFamily
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RelationWithHeadOfFamily()
        {
            Members = new HashSet<Member>();
        }
        [Key]
        public byte RelationWithHeadOfFamilyID { get; set; }

        [StringLength(500)]
        public string RelationDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Member> Members { get; set; }
    }
}