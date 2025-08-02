using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("CandidateAssesments", Schema = "exam")]
    public partial class CandidateAssesment
    {
        [Key]
        public long CandidateAssesmentIID { get; set; }

        public long? CandidateOnlinExamMapID { get; set; }

        public long? SelectedQuestionOptionMapID { get; set; }

        public long? AnswerQuestionOptionMapID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual QuestionOptionMap AnswerQuestionOptionMap { get; set; }

        public virtual CandidateOnlineExamMap CandidateOnlinExamMap { get; set; }

        public virtual QuestionOptionMap SelectedQuestionOptionMap { get; set; }
    }
}