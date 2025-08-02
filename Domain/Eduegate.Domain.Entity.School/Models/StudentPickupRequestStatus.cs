using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("StudentPickupRequestStatuses", Schema = "schools")]
    public partial class StudentPickupRequestStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentPickupRequestStatus()
        {
            StudentPickupRequests = new HashSet<StudentPickupRequest>();
        }
        [Key]
        public byte StudentPickupRequestStatusID { get; set; }

        [StringLength(50)]
        public string StudentPickupRequestStatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentPickupRequest> StudentPickupRequests { get; set; }
    }
}