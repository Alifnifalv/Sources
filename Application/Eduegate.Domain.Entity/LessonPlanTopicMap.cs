namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.LessonPlanTopicMaps")]
    public partial class LessonPlanTopicMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LessonPlanTopicMap()
        {
            LessonPlanTaskMaps = new HashSet<LessonPlanTaskMap>();
            LessonPlanTopicAttachmentMaps = new HashSet<LessonPlanTopicAttachmentMap>();
        }

        [Key]
        public long LessonPlanTopicMapIID { get; set; }

        public long? LessonPlanID { get; set; }

        [StringLength(50)]
        public string LectureCode { get; set; }

        public string Topic { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual LessonPlan LessonPlan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlanTaskMap> LessonPlanTaskMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlanTopicAttachmentMap> LessonPlanTopicAttachmentMaps { get; set; }
    }
}
