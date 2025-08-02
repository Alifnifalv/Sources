using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("QuestionnaireAnswers", Schema = "collaboration")]
    public partial class QuestionnaireAnswer
    {
        public QuestionnaireAnswer()
        {
            MemberQuestionnaireAnswerMaps = new HashSet<MemberQuestionnaireAnswerMap>();
        }

        [Key]
        public long QuestionnaireAnswerIID { get; set; }
        public int? QuestionnaireAnswerTypeID { get; set; }
        public long? QuestionnaireID { get; set; }
        public string Answer { get; set; }
        public string MoreInfo { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("QuestionnaireID")]
        [InverseProperty("QuestionnaireAnswers")]
        public virtual Questionnaire Questionnaire { get; set; }
        [ForeignKey("QuestionnaireAnswerTypeID")]
        [InverseProperty("QuestionnaireAnswers")]
        public virtual QuestionnaireAnswerType QuestionnaireAnswerType { get; set; }
        [InverseProperty("QuestionnaireAnswer")]
        public virtual ICollection<MemberQuestionnaireAnswerMap> MemberQuestionnaireAnswerMaps { get; set; }
    }
}
