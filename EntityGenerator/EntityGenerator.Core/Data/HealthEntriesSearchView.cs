using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class HealthEntriesSearchView
    {
        public long HealthEntryIID { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int? SectionID { get; set; }
        public int? AcademicYearID { get; set; }
        public long? TeacherID { get; set; }
        public byte? SchoolID { get; set; }
        public int? ExamGroupID { get; set; }
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
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(100)]
        public string ExamGroupName { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
        [Required]
        [StringLength(550)]
        public string EmployeeName { get; set; }
    }
}
