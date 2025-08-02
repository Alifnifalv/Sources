using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Community.Models
{
    [Table("QuestionnaireTypes", Schema = "collaboration")]
    public partial class QuestionnaireType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionnaireTypeID { get; set; }

        [StringLength(500)]
        public string TypeName { get; set; }
    }
}