namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LessonPlanTaskMaps", Schema = "schools")]
    public partial class LessonPlanTaskMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LessonPlanTaskMap()
        {
            LessonPlanTaskAttachmentMaps = new HashSet<LessonPlanTaskAttachmentMap>();
        }

        [Key]
        public long LessonPlanTaskMapIID { get; set; }

        public long? LessonPlanTopicMapID { get; set; }

        public string Task { get; set; }

        public byte? TaskTypeID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public long? LessonPlanID { get; set; }

        public int? TimeDuration { get; set; }


        public virtual LessonPlan LessonPlan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlanTaskAttachmentMap> LessonPlanTaskAttachmentMaps { get; set; }

        public virtual TaskType TaskType { get; set; }

        public virtual LessonPlanTopicMap LessonPlanTopicMap { get; set; }
    }
}
