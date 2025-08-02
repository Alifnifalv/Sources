using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OnlineExamQuestions", Schema = "exam")]
    public partial class OnlineExamQuestion
    {
        [Key]
        public long OnlineExamQuestionIID { get; set; }
        public long? CandidateID { get; set; }
        public long? OnlineExamID { get; set; }
        [StringLength(500)]
        public string ExamName { get; set; }
        [StringLength(500)]
        public string ExamDescription { get; set; }
        [StringLength(500)]
        public string GroupName { get; set; }
        public long? QuestionID { get; set; }
        public string Question { get; set; }
        [StringLength(50)]
        public string AnswerType { get; set; }
        public long? QuestionOptionCount { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? SchoolID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("OnlineExamQuestions")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("OnlineExamQuestions")]
        public virtual School School { get; set; }
    }
}
