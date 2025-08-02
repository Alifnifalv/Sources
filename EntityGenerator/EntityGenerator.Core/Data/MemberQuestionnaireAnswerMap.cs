using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MemberQuestionnaireAnswerMaps", Schema = "communities")]
    public partial class MemberQuestionnaireAnswerMap
    {
        [Key]
        public long MemberQuestionnaireAnswerMapIID { get; set; }
        public long? MemberID { get; set; }
        public long? QuestionnaireID { get; set; }
        public long? QuestionnaireAnswerID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("MemberID")]
        [InverseProperty("MemberQuestionnaireAnswerMaps")]
        public virtual Member Member { get; set; }
        [ForeignKey("QuestionnaireID")]
        [InverseProperty("MemberQuestionnaireAnswerMaps")]
        public virtual Questionnaire Questionnaire { get; set; }
        [ForeignKey("QuestionnaireAnswerID")]
        [InverseProperty("MemberQuestionnaireAnswerMaps")]
        public virtual QuestionnaireAnswer QuestionnaireAnswer { get; set; }
    }
}
