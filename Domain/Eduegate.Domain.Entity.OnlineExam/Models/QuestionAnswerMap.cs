using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("QuestionAnswerMaps", Schema = "exam")]
    public partial class QuestionAnswerMap
    {
        [Key]
        public long QuestionAnswerMapIID { get; set; }
        public long? QuestionID { get; set; }
        public long? QuestionOptionMapID { get; set; }
        public virtual Question Question { get; set; }
        public virtual QuestionOptionMap QuestionOptionMap { get; set; }
    }
}