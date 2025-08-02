namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LessonPlanAssessmentMaps", Schema = "schools")]
    public partial class LessonPlanAssessmentMap
    {
        [Key]
        public long AssessmentMapIID { get; set; }
        public long? LessonPlanID { get; set; }
        public string AssessmentName { get; set; }
        public string Description { get; set; }

        public virtual LessonPlan LessonPlan { get; set; }
    }
}
