using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentTeacherDetailView
    {
        public long StudentID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public long? ClassTeacherID { get; set; }
        public long? EmployeeID { get; set; }
        [Required]
        [StringLength(569)]
        public string TeacherName { get; set; }
        [StringLength(500)]
        public string EmployeePhoto { get; set; }
        [StringLength(50)]
        public string GenderDescription { get; set; }
        [StringLength(4000)]
        public string SubjectName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string WorkEmail { get; set; }
    }
}
