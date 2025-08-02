using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ClassTeacherSubjectMapReportView
    {
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
        public long? ClassTeacherID { get; set; }
        [Required]
        [StringLength(502)]
        public string ClassTeacher { get; set; }
        public long? OtherTeacherID { get; set; }
        [Required]
        [StringLength(502)]
        public string OtherTeacherName { get; set; }
        public int? SubjectID { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
    }
}
