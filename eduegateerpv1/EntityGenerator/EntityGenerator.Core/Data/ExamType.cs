using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ExamTypes", Schema = "schools")]
    public partial class ExamType
    {
        public ExamType()
        {
            Exams = new HashSet<Exam>();
            LessonPlans = new HashSet<LessonPlan>();
        }

        [Key]
        public byte ExamTypeID { get; set; }
        [StringLength(50)]
        public string ExamTypeDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ExamTypes")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ExamTypes")]
        public virtual School School { get; set; }
        [InverseProperty("ExamType")]
        public virtual ICollection<Exam> Exams { get; set; }
        [InverseProperty("ExpectedLearningOutcome")]
        public virtual ICollection<LessonPlan> LessonPlans { get; set; }
    }
}
