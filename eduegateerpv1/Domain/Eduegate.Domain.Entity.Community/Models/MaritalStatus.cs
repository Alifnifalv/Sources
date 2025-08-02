using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Community.Models
{
    [Table("MaritalStatuses", Schema = "communities")]
    public partial class MaritalStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MaritalStatus()
        {
            MemberPartners = new HashSet<MemberPartner>();
        }
        [Key]
        public byte MaritalStatusID { get; set; }

        [StringLength(100)]
        public string StatusDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberPartner> MemberPartners { get; set; }
    }
}