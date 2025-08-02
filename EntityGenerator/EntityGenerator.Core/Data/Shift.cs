using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Shifts", Schema = "schools")]
    public partial class Shift
    {
        public Shift()
        {
            TeacherActivities = new HashSet<TeacherActivity>();
        }

        [Key]
        public byte ClassShiftID { get; set; }
        [StringLength(50)]
        public string ShiftDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Shifts")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Shifts")]
        public virtual School School { get; set; }
        [InverseProperty("Shift")]
        public virtual ICollection<TeacherActivity> TeacherActivities { get; set; }
    }
}
