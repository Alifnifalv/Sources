using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LessonPlanTaskAttachmentMaps", Schema = "schools")]
    public partial class LessonPlanTaskAttachmentMap
    {
        [Key]
        public long LessonPlancTaskAttachmentMapIID { get; set; }
        public long? LessonPlanTaskMapID { get; set; }
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

        [ForeignKey("LessonPlanTaskMapID")]
        [InverseProperty("LessonPlanTaskAttachmentMaps")]
        public virtual LessonPlanTaskMap LessonPlanTaskMap { get; set; }
    }
}
