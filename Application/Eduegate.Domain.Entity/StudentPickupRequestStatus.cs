namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentPickupRequestStatuses")]
    public partial class StudentPickupRequestStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentPickupRequestStatus()
        {
            StudentPickupRequests = new HashSet<StudentPickupRequest>();
        }

        public byte StudentPickupRequestStatusID { get; set; }

        [StringLength(50)]
        public string StudentPickupRequestStatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentPickupRequest> StudentPickupRequests { get; set; }
    }
}
