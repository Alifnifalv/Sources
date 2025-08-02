using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LessonPlanTopicMaps", Schema = "schools")]
    public partial class LessonPlanTopicMap
    {
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? Period { get; set; }

        [ForeignKey("LessonPlanID")]
        [InverseProperty("LessonPlanTopicMaps")]
        public virtual LessonPlan LessonPlan { get; set; }
        [InverseProperty("LessonPlanTopicMap")]
        public virtual ICollection<LessonPlanTaskMap> LessonPlanTaskMaps { get; set; }
        [InverseProperty("LessonPlanTopicMap")]
        public virtual ICollection<LessonPlanTopicAttachmentMap> LessonPlanTopicAttachmentMaps { get; set; }
    }
}
