using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassClassTeacherMaps", Schema = "schools")]
    public partial class ClassClassTeacherMap
    {
        public ClassClassTeacherMap()
        {
            ClassAssociateTeacherMaps = new HashSet<ClassAssociateTeacherMap>();
            ClassTeacherMaps = new HashSet<ClassTeacherMap>();
        }

        [Key]
        public long ClassClassTeacherMapIID { get; set; }
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
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? SubjectID { get; set; }
        public long? CoordinatorID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ClassClassTeacherMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("ClassClassTeacherMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("CoordinatorID")]
        [InverseProperty("ClassClassTeacherMapCoordinators")]
        public virtual Employee Coordinator { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ClassClassTeacherMaps")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("ClassClassTeacherMaps")]
        public virtual Section Section { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("ClassClassTeacherMaps")]
        public virtual Subject Subject { get; set; }
        [ForeignKey("TeacherID")]
        [InverseProperty("ClassClassTeacherMapTeachers")]
        public virtual Employee Teacher { get; set; }
        [InverseProperty("ClassClassTeacherMap")]
        public virtual ICollection<ClassAssociateTeacherMap> ClassAssociateTeacherMaps { get; set; }
        [InverseProperty("ClassClassTeacherMap")]
        public virtual ICollection<ClassTeacherMap> ClassTeacherMaps { get; set; }
    }
}
