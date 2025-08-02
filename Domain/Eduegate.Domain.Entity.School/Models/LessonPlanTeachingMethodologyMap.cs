namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LessonPlanTeachingMethodologyMaps", Schema = "schools")]
    public partial class LessonPlanTeachingMethodologyMap
    {
        [Key]
        public long TeachingMethodologyMapIID { get; set; }
        public long? LessonPlanID { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public byte? Duration { get; set; }
        public virtual LessonPlan LessonPlan { get; set; }
    }
}
