namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.Questions")]
    public partial class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            CandidateAnswers = new HashSet<CandidateAnswer>();
            OnlineExamQuestionMaps = new HashSet<OnlineExamQuestionMap>();
            OnlineExamResultQuestionMaps = new HashSet<OnlineExamResultQuestionMap>();
            QuestionAnswerMaps = new HashSet<QuestionAnswerMap>();
            QuestionOptionMaps = new HashSet<QuestionOptionMap>();
        }

        [Key]
        public long QuestionIID { get; set; }

        public string Description { get; set; }

        public byte? AnswerTypeID { get; set; }

        public int? SubjectID { get; set; }

        public int? QuestionGroupID { get; set; }

        public decimal? Points { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual AnswerType AnswerType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateAnswer> CandidateAnswers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnlineExamQuestionMap> OnlineExamQuestionMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnlineExamResultQuestionMap> OnlineExamResultQuestionMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionAnswerMap> QuestionAnswerMaps { get; set; }

        public virtual QuestionGroup QuestionGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionOptionMap> QuestionOptionMaps { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Subject Subject1 { get; set; }
    }
}
