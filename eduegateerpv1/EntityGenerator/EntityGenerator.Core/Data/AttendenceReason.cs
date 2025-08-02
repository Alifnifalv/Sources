using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AttendenceReasons", Schema = "schools")]
    public partial class AttendenceReason
    {
        public AttendenceReason()
        {
            StaffAttendences = new HashSet<StaffAttendence>();
            StudentAttendences = new HashSet<StudentAttendence>();
        }

        [Key]
        public int AttendenceReasonID { get; set; }
        [StringLength(500)]
        public string Reason { get; set; }

        [InverseProperty("AttendenceReason")]
        public virtual ICollection<StaffAttendence> StaffAttendences { get; set; }
        [InverseProperty("AttendenceReason")]
        public virtual ICollection<StudentAttendence> StudentAttendences { get; set; }
    }
}
