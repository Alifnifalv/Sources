using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Exams_20220317
    {
        public long ExamIID { get; set; }
        [StringLength(100)]
        public string ExamDescription { get; set; }
        [StringLength(100)]
        public string ExamNumber { get; set; }
        public byte? ExamTypeID { get; set; }
        public int? MarkGradeID { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? ExamGroupID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ConversionFactor { get; set; }
        public bool? IsProgressCard { get; set; }
        public bool? IsAnnualExam { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string ProgressCardHeader { get; set; }
        public bool? IsAssessment { get; set; }
        public bool? IsConverted { get; set; }
    }
}
