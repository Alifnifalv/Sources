using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OnlineExams", Schema = "exam")]
    public partial class OnlineExam
    {
        public OnlineExam()
        {
            CandidateOnlineExamMaps = new HashSet<CandidateOnlineExamMap>();
            ExamQuestionGroupMaps = new HashSet<ExamQuestionGroupMap>();
            OnlineExamQuestionMaps = new HashSet<OnlineExamQuestionMap>();
            OnlineExamResults = new HashSet<OnlineExamResult>();
            OnlineExamSubjectMaps = new HashSet<OnlineExamSubjectMap>();
        }

        [Key]
        public long OnlineExamIID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public byte? QuestionSelectionID { get; set; }
        public double? MinimumDuration { get; set; }
        public double? MaximumDuration { get; set; }
        public double? PassPercentage { get; set; }
        public int? PassNos { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MaximumMarks { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MinimumMarks { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? ClassID { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? SchoolID { get; set; }
        public byte? OnlineExamTypeID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("OnlineExams")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("OnlineExams")]
        public virtual Class Class { get; set; }
        [ForeignKey("OnlineExamTypeID")]
        [InverseProperty("OnlineExams")]
        public virtual OnlineExamType OnlineExamType { get; set; }
        [ForeignKey("QuestionSelectionID")]
        [InverseProperty("OnlineExams")]
        public virtual QuestionSelection QuestionSelection { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("OnlineExams")]
        public virtual School School { get; set; }
        [InverseProperty("OnlineExam")]
        public virtual ICollection<CandidateOnlineExamMap> CandidateOnlineExamMaps { get; set; }
        [InverseProperty("OnlineExam")]
        public virtual ICollection<ExamQuestionGroupMap> ExamQuestionGroupMaps { get; set; }
        [InverseProperty("OnlineExam")]
        public virtual ICollection<OnlineExamQuestionMap> OnlineExamQuestionMaps { get; set; }
        [InverseProperty("OnlineExam")]
        public virtual ICollection<OnlineExamResult> OnlineExamResults { get; set; }
        [InverseProperty("OnlineExam")]
        public virtual ICollection<OnlineExamSubjectMap> OnlineExamSubjectMaps { get; set; }
    }
}
