namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AttendenceReasons", Schema = "schools")]
    public partial class AttendenceReason
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttendenceReason()
        {
            StaffAttendences = new HashSet<StaffAttendence>();
            StudentAttendences = new HashSet<StudentAttendence>();
        }
        [Key]

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
