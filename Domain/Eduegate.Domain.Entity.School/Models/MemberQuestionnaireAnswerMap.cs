namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MemberQuestionnaireAnswerMaps", Schema = "schools")]
    public partial class MemberQuestionnaireAnswerMap
    {
        [Key]
        public long MemberQuestionnaireAnswerMapIID { get; set; }

        public long? MemberID { get; set; }

        public long? QuestionnaireID { get; set; }

        public long? QuestionnaireAnswerID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual QuestionnaireAnswer QuestionnaireAnswer { get; set; }

        public virtual Questionnaire Questionnaire { get; set; }

        //public virtual Member Member { get; set; }
    }
}
