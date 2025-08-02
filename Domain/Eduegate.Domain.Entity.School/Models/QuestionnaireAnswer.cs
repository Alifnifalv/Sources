namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("QuestionnaireAnswers", Schema = "collaboration")]
    public partial class QuestionnaireAnswer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberQuestionnaireAnswerMap> MemberQuestionnaireAnswerMaps { get; set; }

        public virtual QuestionnaireAnswerType QuestionnaireAnswerType { get; set; }

        public virtual Questionnaire Questionnaire { get; set; }
    }
}
