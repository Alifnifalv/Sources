using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Questionnaires", Schema = "collaboration")]
    public partial class Questionnaire
    {
        public Questionnaire()
        {
            MemberQuestionnaireAnswerMaps = new HashSet<MemberQuestionnaireAnswerMap>();
            QuestionnaireAnswers = new HashSet<QuestionnaireAnswer>();
        }

        [Key]
        public long QuestionnaireIID { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public int? QuestionnaireAnswerTypeID { get; set; }
        [StringLength(2000)]
        public string MoreInfo { get; set; }

        [ForeignKey("QuestionnaireAnswerTypeID")]
        [InverseProperty("Questionnaires")]
        public virtual QuestionnaireAnswerType QuestionnaireAnswerType { get; set; }
        [InverseProperty("Questionnaire")]
        public virtual ICollection<MemberQuestionnaireAnswerMap> MemberQuestionnaireAnswerMaps { get; set; }
        [InverseProperty("Questionnaire")]
        public virtual ICollection<QuestionnaireAnswer> QuestionnaireAnswers { get; set; }
    }
}
