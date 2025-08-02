using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CandidateAnswers", Schema = "exam")]
    public partial class CandidateAnswer
    {
        [Key]
        public long CandidateAnswerIID { get; set; }
        public long? CandidateOnlineExamMapID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfAnswer { get; set; }
        public string Comments { get; set; }
        public long? QuestionOptionMapID { get; set; }
        public string OtherDetails { get; set; }
        public long? CandidateID { get; set; }
        public string OtherAnswers { get; set; }
        public long? QuestionID { get; set; }

        [ForeignKey("CandidateID")]
        [InverseProperty("CandidateAnswers")]
        public virtual Candidate Candidate { get; set; }
        [ForeignKey("CandidateOnlineExamMapID")]
        [InverseProperty("CandidateAnswers")]
        public virtual CandidateOnlineExamMap CandidateOnlineExamMap { get; set; }
        [ForeignKey("QuestionID")]
        [InverseProperty("CandidateAnswers")]
        public virtual Question Question { get; set; }
        [ForeignKey("QuestionOptionMapID")]
        [InverseProperty("CandidateAnswers")]
        public virtual QuestionOptionMap QuestionOptionMap { get; set; }
    }
}
