using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Community.Models
{
    [Table("QuestionnaireAnswerTypes", Schema = "collaboration")]
    public partial class QuestionnaireAnswerType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuestionnaireAnswerType()
        {
            QuestionnaireAnswers = new HashSet<QuestionnaireAnswer>();
            Questionnaires = new HashSet<Questionnaire>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionnaireAnswerTypeID { get; set; }

        [StringLength(500)]
        public string TypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionnaireAnswer> QuestionnaireAnswers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Questionnaire> Questionnaires { get; set; }
    }
}