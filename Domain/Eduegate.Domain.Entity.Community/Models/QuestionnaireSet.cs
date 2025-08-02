using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Community.Models
{
    [Table("QuestionnaireSets", Schema = "collaboration")]
    public partial class QuestionnaireSet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionnaireSetID { get; set; }

        [StringLength(100)]
        public string QuestionnaireSetName { get; set; }
    }
}