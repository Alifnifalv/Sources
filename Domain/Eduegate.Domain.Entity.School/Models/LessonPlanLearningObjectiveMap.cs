using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("LessonPlanLearningObjectiveMaps", Schema = "schools")]
    public partial class LessonPlanLearningObjectiveMap
    {
        [Key]
        public long LessonPlanLearningObjectiveMapIID { get; set; }

        public long? LessonPlanID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public string ObjectiveName { get; set; }

        public byte? LessonLearningObjectiveID { get; set; }

        public virtual LessonPlan LessonPlan { get; set; }
    }
}