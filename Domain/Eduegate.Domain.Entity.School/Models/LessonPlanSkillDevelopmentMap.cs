namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LessonPlanSkillDevelopmentMaps", Schema = "schools")]
    public partial class LessonPlanSkillDevelopmentMap
    {
        [Key]
        public long SkillDevelopmentMapIID { get; set; }
        public long? LessonPlanID { get; set; }
        public string Description { get; set; }

        public virtual LessonPlan LessonPlan { get; set; }
    }
}
