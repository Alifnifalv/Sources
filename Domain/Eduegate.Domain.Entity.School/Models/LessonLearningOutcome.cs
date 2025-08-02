using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("LessonLearningOutcomes", Schema = "schools")]
    public partial class LessonLearningOutcome
    {
        [Key]
        public byte LessonLearningOutcomeID { get; set; }

        [StringLength(50)]
        public string LessonLearningOutcomeName { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }
    }
}
