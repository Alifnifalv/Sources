using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Questions", Schema = "exam")]
    public partial class Question
    {
        public Question()
        {
            CandidateAnswers = new HashSet<CandidateAnswer>();
            OnlineExamQuestionMaps = new HashSet<OnlineExamQuestionMap>();
            OnlineExamResultQuestionMaps = new HashSet<OnlineExamResultQuestionMap>();
            QuestionOptionMaps = new HashSet<QuestionOptionMap>();
        }

        [Key]
        public long QuestionIID { get; set; }
        public string Description { get; set; }
        public byte? AnswerTypeID { get; set; }
        public int? SubjectID { get; set; }
        public int? QuestionGroupID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Points { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public string DocFile { get; set; }
        public long? PassageQuestionID { get; set; }

        [ForeignKey("AnswerTypeID")]
        [InverseProperty("Questions")]
        public virtual AnswerType AnswerType { get; set; }
        [ForeignKey("PassageQuestionID")]
        [InverseProperty("Questions")]
        public virtual PassageQuestion PassageQuestion { get; set; }
        [ForeignKey("QuestionGroupID")]
        [InverseProperty("Questions")]
        public virtual QuestionGroup QuestionGroup { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("Questions")]
        public virtual Subject Subject { get; set; }
        [InverseProperty("Question")]
        public virtual ICollection<CandidateAnswer> CandidateAnswers { get; set; }
        [InverseProperty("Question")]
        public virtual ICollection<OnlineExamQuestionMap> OnlineExamQuestionMaps { get; set; }
        [InverseProperty("Question")]
        public virtual ICollection<OnlineExamResultQuestionMap> OnlineExamResultQuestionMaps { get; set; }
        [InverseProperty("Question")]
        public virtual ICollection<QuestionOptionMap> QuestionOptionMaps { get; set; }
    }
}
