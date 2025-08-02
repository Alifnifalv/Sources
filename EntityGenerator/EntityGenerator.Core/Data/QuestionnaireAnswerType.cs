using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("QuestionnaireAnswerTypes", Schema = "collaboration")]
    public partial class QuestionnaireAnswerType
    {
        public QuestionnaireAnswerType()
        {
            QuestionnaireAnswers = new HashSet<QuestionnaireAnswer>();
            Questionnaires = new HashSet<Questionnaire>();
        }

        [Key]
        public int QuestionnaireAnswerTypeID { get; set; }
        [StringLength(500)]
        public string TypeName { get; set; }

        [InverseProperty("QuestionnaireAnswerType")]
        public virtual ICollection<QuestionnaireAnswer> QuestionnaireAnswers { get; set; }
        [InverseProperty("QuestionnaireAnswerType")]
        public virtual ICollection<Questionnaire> Questionnaires { get; set; }
    }
}
