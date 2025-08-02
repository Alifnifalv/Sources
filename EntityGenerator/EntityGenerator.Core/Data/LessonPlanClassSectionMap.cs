using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LessonPlanClassSectionMaps", Schema = "schools")]
    public partial class LessonPlanClassSectionMap
    {
        [Key]
        public long LessonPlanClassSectionMapIID { get; set; }
        public long? LessonPlanID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ClassID")]
        [InverseProperty("LessonPlanClassSectionMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("LessonPlanID")]
        [InverseProperty("LessonPlanClassSectionMaps")]
        public virtual LessonPlan LessonPlan { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("LessonPlanClassSectionMaps")]
        public virtual Section Section { get; set; }
    }
}
