using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("QuestionOptionMaps", Schema = "exam")]
    public partial class QuestionOptionMap
    {
        public QuestionOptionMap()
        {
            CandidateAnswers = new HashSet<CandidateAnswer>();
            CandidateAssesmentAnswerQuestionOptionMaps = new HashSet<CandidateAssesment>();
            CandidateAssesmentSelectedQuestionOptionMaps = new HashSet<CandidateAssesment>();
        }

        [Key]
        public long QuestionOptionMapIID { get; set; }
        public string OptionText { get; set; }
        public long? QuestionID { get; set; }
        public string ImageName { get; set; }
        public long? ContentID { get; set; }
        public int? OrderNo { get; set; }
        public bool? IsCorrectAnswer { get; set; }

        [ForeignKey("QuestionID")]
        [InverseProperty("QuestionOptionMaps")]
        public virtual Question Question { get; set; }
        [InverseProperty("QuestionOptionMap")]
        public virtual ICollection<CandidateAnswer> CandidateAnswers { get; set; }
        [InverseProperty("AnswerQuestionOptionMap")]
        public virtual ICollection<CandidateAssesment> CandidateAssesmentAnswerQuestionOptionMaps { get; set; }
        [InverseProperty("SelectedQuestionOptionMap")]
        public virtual ICollection<CandidateAssesment> CandidateAssesmentSelectedQuestionOptionMaps { get; set; }
    }
}
