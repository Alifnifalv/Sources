using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Community.Models
{
    [Table("BloodGroups", Schema = "mutual")]
    public partial class BloodGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BloodGroup()
        {
            MemberHealths = new HashSet<MemberHealth>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BloodGroupID { get; set; }

        [StringLength(100)]
        public string BloodGroupName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberHealth> MemberHealths { get; set; }
    }
}