using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LessonPlanStatuses", Schema = "schools")]
    public partial class LessonPlanStatus
    {
        public LessonPlanStatus()
        {
            LessonPlans = new HashSet<LessonPlan>();
        }

        [Key]
        public byte LessonPlanStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("LessonPlanStatus")]
        public virtual ICollection<LessonPlan> LessonPlans { get; set; }
    }
}
