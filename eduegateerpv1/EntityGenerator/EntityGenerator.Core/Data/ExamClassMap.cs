using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ExamClassMaps", Schema = "schools")]
    [Index("ExamID", Name = "IDX_EXAMCLASSMAPS_EXAMID")]
    [Index("ClassID", "SectionID", Name = "IDX_EXAMCLASSMAPS_SECTIONID_CLASSID")]
    public partial class ExamClassMap
    {
        [Key]
        public long ExamClassMapIID { get; set; }
        public long? ExamScheduleID { get; set; }
        public long? ExamID { get; set; }
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
        [InverseProperty("ExamClassMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("ExamID")]
        [InverseProperty("ExamClassMaps")]
        public virtual Exam Exam { get; set; }
        [ForeignKey("ExamScheduleID")]
        [InverseProperty("ExamClassMaps")]
        public virtual ExamSchedule ExamSchedule { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("ExamClassMaps")]
        public virtual Section Section { get; set; }
    }
}
