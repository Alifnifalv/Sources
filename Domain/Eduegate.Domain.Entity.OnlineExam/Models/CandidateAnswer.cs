using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("CandidateAnswers", Schema = "exam")]
    public partial class CandidateAnswer
    {
        [Key]
        public long CandidateAnswerIID { get; set; }

        public long? CandidateOnlineExamMapID { get; set; }

        public DateTime? DateOfAnswer { get; set; }

        public string Comments { get; set; }

        public long? QuestionOptionMapID { get; set; }

        public string OtherDetails { get; set; }

        public long? CandidateID { get; set; }

        public string OtherAnswers { get; set; }

        public long? QuestionID { get; set; }

        public virtual Candidate Candidate { get; set; }

        public virtual CandidateOnlineExamMap CandidateOnlineExamMap { get; set; }

        public virtual Question Question { get; set; }

        public virtual QuestionOptionMap QuestionOptionMap { get; set; }
    }
}