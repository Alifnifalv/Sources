using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("QuestionOptionMaps", Schema = "exam")]
    public partial class QuestionOptionMap
    {
        public QuestionOptionMap()
        {
            CandidateAnswers = new HashSet<CandidateAnswer>();
            CandidateAssesmentAnswerQuestionOptionMaps = new HashSet<CandidateAssesment>();
            CandidateAssesmentSelectedQuestionOptionMaps = new HashSet<CandidateAssesment>();
            //QuestionAnswerMaps = new HashSet<QuestionAnswerMap>();
        }

        [Key]
        public long QuestionOptionMapIID { get; set; }
        public string OptionText { get; set; }
        public long? QuestionID { get; set; }
        public string ImageName { get; set; }
        public long? ContentID { get; set; }
        public int? OrderNo { get; set; }

        public bool? IsCorrectAnswer { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<CandidateAnswer> CandidateAnswers { get; set; }
        public virtual ICollection<CandidateAssesment> CandidateAssesmentAnswerQuestionOptionMaps { get; set; }
        public virtual ICollection<CandidateAssesment> CandidateAssesmentSelectedQuestionOptionMaps { get; set; }
        //public virtual ICollection<QuestionAnswerMap> QuestionAnswerMaps { get; set; }
    }
}