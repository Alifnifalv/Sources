using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Teacher_Contract
    {
        [StringLength(255)]
        public string Teacher { get; set; }
        [StringLength(255)]
        public string Class { get; set; }
        [StringLength(255)]
        public string Group { get; set; }
        [StringLength(255)]
        public string Subject { get; set; }
        [StringLength(255)]
        public string Length { get; set; }
        public double? Lessons_week { get; set; }
        [StringLength(255)]
        public string Available_classrooms { get; set; }
        [StringLength(255)]
        public string Cycle { get; set; }
        [StringLength(255)]
        public string More_teachers { get; set; }
        [StringLength(255)]
        public string Classrooms { get; set; }
        [StringLength(255)]
        public string F11 { get; set; }
        public int TC_ID { get; set; }
        public int? TeacherID { get; set; }
    }
}
