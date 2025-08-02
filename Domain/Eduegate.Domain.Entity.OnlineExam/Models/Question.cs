using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("Questions", Schema = "exam")]
    public partial class Question
    {
        public Question()
        {
            CandidateAnswers = new HashSet<CandidateAnswer>();
            OnlineExamQuestionMaps = new HashSet<OnlineExamQuestionMap>();
            OnlineExamResultQuestionMaps = new HashSet<OnlineExamResultQuestionMap>();
            //QuestionAnswerMaps = new HashSet<QuestionAnswerMap>();
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
        public string DocFile { get; set; }
        public long? PassageQuestionID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        //public byte[] TimeStamps { get; set; }

        public virtual AnswerType AnswerType { get; set; }

        public virtual QuestionGroup QuestionGroup { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual ICollection<CandidateAnswer> CandidateAnswers { get; set; }

        public virtual ICollection<OnlineExamQuestionMap> OnlineExamQuestionMaps { get; set; }

        public virtual ICollection<OnlineExamResultQuestionMap> OnlineExamResultQuestionMaps { get; set; }

        //public virtual ICollection<QuestionAnswerMap> QuestionAnswerMaps { get; set; }

        public virtual ICollection<QuestionOptionMap> QuestionOptionMaps { get; set; }
        public virtual PassageQuestion PassageQuestion { get; set; }

    }
}