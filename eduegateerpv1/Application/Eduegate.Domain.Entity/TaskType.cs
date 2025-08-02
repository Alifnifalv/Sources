namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.TaskTypes")]
    public partial class TaskType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaskType()
        {
            AgendaTaskMaps = new HashSet<AgendaTaskMap>();
            LessonPlanTaskMaps = new HashSet<LessonPlanTaskMap>();
        }

        public byte TaskTypeID { get; set; }

        [Column("TaskType")]
        [StringLength(50)]
        public string TaskType1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaTaskMap> AgendaTaskMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlanTaskMap> LessonPlanTaskMaps { get; set; }
    }
}
