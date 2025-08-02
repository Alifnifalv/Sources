using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Community.Models
{
    [Table("Questionnaires", Schema = "collaboration")]
    public partial class Questionnaire
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Questionnaire()
        {
            QuestionnaireAnswers = new HashSet<QuestionnaireAnswer>();
            MemberQuestionnaireAnswerMaps = new HashSet<MemberQuestionnaireAnswerMap>();
        }

        [Key]
        public long QuestionnaireIID { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public int? QuestionnaireAnswerTypeID { get; set; }

        [StringLength(2000)]
        public string MoreInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionnaireAnswer> QuestionnaireAnswers { get; set; }

        public virtual QuestionnaireAnswerType QuestionnaireAnswerType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberQuestionnaireAnswerMap> MemberQuestionnaireAnswerMaps { get; set; }
    }
}