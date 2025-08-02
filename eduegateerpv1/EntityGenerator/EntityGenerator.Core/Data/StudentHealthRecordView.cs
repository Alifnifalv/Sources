using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentHealthRecordView
    {
        public long HealthEntryStudentMapIID { get; set; }
        [StringLength(2001)]
        [Unicode(false)]
        public string AcademicYear { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(100)]
        public string ExamGroupName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Height { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Weight { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BMS { get; set; }
        public string Vision { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BMI { get; set; }
        public string Remarks { get; set; }
        public long? StudentID { get; set; }
    }
}
