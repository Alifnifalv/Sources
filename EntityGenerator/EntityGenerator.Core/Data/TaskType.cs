using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TaskTypes", Schema = "schools")]
    public partial class TaskType
    {
        public TaskType()
        {
            AgendaTaskMaps = new HashSet<AgendaTaskMap>();
            LessonPlanTaskMaps = new HashSet<LessonPlanTaskMap>();
        }

        [Key]
        public byte TaskTypeID { get; set; }
        [Column("TaskType")]
        [StringLength(50)]
        public string TaskType1 { get; set; }

        [InverseProperty("TaskType")]
        public virtual ICollection<AgendaTaskMap> AgendaTaskMaps { get; set; }
        [InverseProperty("TaskType")]
        public virtual ICollection<LessonPlanTaskMap> LessonPlanTaskMaps { get; set; }
    }
}
