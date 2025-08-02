using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LessonPlanLearningObjectiveMaps", Schema = "schools")]
    public partial class LessonPlanLearningObjectiveMap
    {
        [Key]
        public long LessonPlanLearningObjectiveMapIID { get; set; }
        public long? LessonPlanID { get; set; }
        public byte? LessonLearningObjectiveID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("LessonLearningObjectiveID")]
        [InverseProperty("LessonPlanLearningObjectiveMaps")]
        public virtual LessonLearningObjective LessonLearningObjective { get; set; }
        [ForeignKey("LessonPlanID")]
        [InverseProperty("LessonPlanLearningObjectiveMaps")]
        public virtual LessonPlan LessonPlan { get; set; }
    }
}
