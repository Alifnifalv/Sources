using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LessonPlanAttachmentMaps", Schema = "schools")]
    public partial class LessonPlanAttachmentMap
    {
        [Key]
        public long LessonPlanAttachmentMapIID { get; set; }
        public long? LessonPlanID { get; set; }
        public long? AttachmentReferenceID { get; set; }
        [StringLength(50)]
        public string AttachmentName { get; set; }
        [StringLength(1000)]
        public string AttachmentDescription { get; set; }
        public string Notes { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("LessonPlanID")]
        [InverseProperty("LessonPlanAttachmentMaps")]
        public virtual LessonPlan LessonPlan { get; set; }
    }
}
