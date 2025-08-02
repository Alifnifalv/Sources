using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LessonPlanLearningOutcomeMaps", Schema = "schools")]
    public partial class LessonPlanLearningOutcomeMap
    {
        [Key]
        public long LessonPlanLearningOutcomeMapIID { get; set; }
        public long? LessonPlanID { get; set; }
        public byte? LessonLearningOutcomeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("LessonLearningOutcomeID")]
        [InverseProperty("LessonPlanLearningOutcomeMaps")]
        public virtual LessonLearningOutcome LessonLearningOutcome { get; set; }
        [ForeignKey("LessonPlanID")]
        [InverseProperty("LessonPlanLearningOutcomeMaps")]
        public virtual LessonPlan LessonPlan { get; set; }
    }
}
