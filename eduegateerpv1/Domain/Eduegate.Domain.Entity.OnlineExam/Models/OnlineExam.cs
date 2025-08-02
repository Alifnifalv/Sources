using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("OnlineExams", Schema = "exam")]
    public partial class OnlineExam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public byte QuestionSelectionID { get; set; }

        public double? MinimumDuration { get; set; }

        public double? MaximumDuration { get; set; }

        public double? PassPercentage { get; set; }

        public int? PassNos { get; set; }

        public decimal? MaximumMarks { get; set; }

        public decimal? MinimumMarks { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public int? ClassID { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? SchoolID { get; set; }

        public byte? OnlineExamTypeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateOnlineExamMap> CandidateOnlineExamMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamQuestionGroupMap> ExamQuestionGroupMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnlineExamQuestionMap> OnlineExamQuestionMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnlineExamResult> OnlineExamResults { get; set; }

        public virtual OnlineExamType OnlineExamType { get; set; }

        public virtual QuestionSelection QuestionSelection { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual School School { get; set; }

        public virtual Class Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnlineExamSubjectMap> OnlineExamSubjectMaps { get; set; }
    }
}