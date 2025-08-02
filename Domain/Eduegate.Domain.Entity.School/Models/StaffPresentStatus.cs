using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("StaffPresentStatuses", Schema = "schools")]
    public partial class StaffPresentStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StaffPresentStatus()
        {
            StaffAttendences = new HashSet<StaffAttendence>();
        }
        [Key]
        public byte StaffPresentStatusID { get; set; }

        [StringLength(50)]
        public string StatusDescription { get; set; }

        [StringLength(20)]
        public string StatusTitle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffAttendence> StaffAttendences { get; set; }
    }
}