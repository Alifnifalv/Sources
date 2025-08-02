using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Community.Models
{
    [Table("OccupationTypes", Schema = "communities")]
    public partial class OccupationType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OccupationType()
        {
            SocialServices = new HashSet<SocialService>();
        }
        [Key]
        public byte OccupationTypeID { get; set; }

        [StringLength(100)]
        public string OccupationDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SocialService> SocialServices { get; set; }
    }
}