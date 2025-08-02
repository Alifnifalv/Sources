using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassTeacherMaps", Schema = "schools")]
    [Index("ClassClassTeacherMapID", "TeacherID", Name = "_dta_index_ClassTeacherMaps_7_2096882687__K14_K4")]
    [Index("ClassID", "TeacherID", Name = "_dta_index_ClassTeacherMaps_7_2096882687__K2_K4_1_3_5_6_7_8_9_10_11_12_13_14_15")]
    [Index("TeacherID", Name = "idx_ClassTeacherMapsTeacherID")]
    public partial class ClassTeacherMap
    {
        [Key]
        public long ClassTeacherMapIID { get; set; }
        public int? ClassID { get; set; }
        public long? ClassTeacherID { get; set; }
        public long? TeacherID { get; set; }
        public int? SectionID { get; set; }
        public long? EmployeeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public long? ClassClassTeacherMapID { get; set; }
        public int? SubjectID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ClassTeacherMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("ClassTeacherMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("ClassClassTeacherMapID")]
        [InverseProperty("ClassTeacherMaps")]
        public virtual ClassClassTeacherMap ClassClassTeacherMap { get; set; }
        [ForeignKey("ClassTeacherID")]
        [InverseProperty("ClassTeacherMapClassTeachers")]
        public virtual Employee ClassTeacher { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("ClassTeacherMapEmployees")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ClassTeacherMaps")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("ClassTeacherMaps")]
        public virtual Section Section { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("ClassTeacherMaps")]
        public virtual Subject Subject { get; set; }
        [ForeignKey("TeacherID")]
        [InverseProperty("ClassTeacherMapTeachers")]
        public virtual Employee Teacher { get; set; }
    }
}
