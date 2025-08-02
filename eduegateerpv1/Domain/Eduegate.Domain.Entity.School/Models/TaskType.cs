namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TaskTypes", Schema = "schools")]
    public partial class TaskType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaskType()
        {
            LessonPlanTaskMaps = new HashSet<LessonPlanTaskMap>();
            AgendaTaskMaps = new HashSet<AgendaTaskMap>();
        }
        [Key]
        public byte TaskTypeID { get; set; }

        [Column("TaskType")]
        [StringLength(50)]
        public string TaskType1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlanTaskMap> LessonPlanTaskMaps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaTaskMap> AgendaTaskMaps { get; set; }
    }
}