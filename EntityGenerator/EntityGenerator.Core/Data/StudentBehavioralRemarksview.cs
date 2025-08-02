using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentBehavioralRemarksview
    {
        public long RemarksEntryIID { get; set; }
        public long? StudentID { get; set; }
        [StringLength(251)]
        public string Student { get; set; }
        [Required]
        [StringLength(553)]
        public string Teacher { get; set; }
        [StringLength(50)]
        public string Class { get; set; }
        [StringLength(50)]
        public string Section { get; set; }
        public string Remarks { get; set; }
        [StringLength(2001)]
        [Unicode(false)]
        public string AcademicYear { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public long? TeacherID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? ExamGroupID { get; set; }
        [StringLength(100)]
        public string ExamGroupName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
    }
}
