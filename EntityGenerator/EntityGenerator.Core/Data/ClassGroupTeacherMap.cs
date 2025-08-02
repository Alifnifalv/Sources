using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassGroupTeacherMaps", Schema = "schools")]
    public partial class ClassGroupTeacherMap
    {
        [Key]
        public long ClassGroupTeacherMapIID { get; set; }
        public long? ClassGroupID { get; set; }
        public long? TeacherID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public bool? IsHeadTeacher { get; set; }

        [ForeignKey("ClassGroupID")]
        [InverseProperty("ClassGroupTeacherMaps")]
        public virtual ClassGroup ClassGroup { get; set; }
        [ForeignKey("TeacherID")]
        [InverseProperty("ClassGroupTeacherMaps")]
        public virtual Employee Teacher { get; set; }
    }
}
