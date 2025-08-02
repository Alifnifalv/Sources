using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LessonPlanTaskMaps", Schema = "schools")]
    public partial class LessonPlanTaskMap
    {
        public LessonPlanTaskMap()
        {
            LessonPlanTaskAttachmentMaps = new HashSet<LessonPlanTaskAttachmentMap>();
        }

        [Key]
        public long LessonPlanTaskMapIID { get; set; }
        public long? LessonPlanTopicMapID { get; set; }
        public string Task { get; set; }
        public byte? TaskTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? LessonPlanID { get; set; }

        [ForeignKey("LessonPlanID")]
        [InverseProperty("LessonPlanTaskMaps")]
        public virtual LessonPlan LessonPlan { get; set; }
        [ForeignKey("LessonPlanTopicMapID")]
        [InverseProperty("LessonPlanTaskMaps")]
        public virtual LessonPlanTopicMap LessonPlanTopicMap { get; set; }
        [ForeignKey("TaskTypeID")]
        [InverseProperty("LessonPlanTaskMaps")]
        public virtual TaskType TaskType { get; set; }
        [InverseProperty("LessonPlanTaskMap")]
        public virtual ICollection<LessonPlanTaskAttachmentMap> LessonPlanTaskAttachmentMaps { get; set; }
    }
}
