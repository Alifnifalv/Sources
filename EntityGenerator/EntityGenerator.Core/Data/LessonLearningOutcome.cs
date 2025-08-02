using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LessonLearningOutcomes", Schema = "schools")]
    public partial class LessonLearningOutcome
    {
        public LessonLearningOutcome()
        {
            LessonPlanLearningOutcomeMaps = new HashSet<LessonPlanLearningOutcomeMap>();
        }

        [Key]
        public byte LessonLearningOutcomeID { get; set; }
        [StringLength(50)]
        public string LessonLearningOutcomeName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("LessonLearningOutcome")]
        public virtual ICollection<LessonPlanLearningOutcomeMap> LessonPlanLearningOutcomeMaps { get; set; }
    }
}
