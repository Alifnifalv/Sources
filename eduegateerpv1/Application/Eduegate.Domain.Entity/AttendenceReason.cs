namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.AttendenceReasons")]
    public partial class AttendenceReason
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttendenceReason()
        {
            StaffAttendences = new HashSet<StaffAttendence>();
            StudentAttendences = new HashSet<StudentAttendence>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AttendenceReasonID { get; set; }

        [StringLength(500)]
        public string Reason { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffAttendence> StaffAttendences { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentAttendence> StudentAttendences { get; set; }
    }
}
