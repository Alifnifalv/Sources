using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassGroups", Schema = "schools")]
    public partial class ClassGroup
    {
        public ClassGroup()
        {
            ClassClassGroupMaps = new HashSet<ClassClassGroupMap>();
            ClassGroupTeacherMaps = new HashSet<ClassGroupTeacherMap>();
        }

        [Key]
        public long ClassGroupID { get; set; }
        public long? HeadTeacherID { get; set; }
        public string GroupDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? SubjectID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ClassGroups")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("HeadTeacherID")]
        [InverseProperty("ClassGroups")]
        public virtual Employee HeadTeacher { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ClassGroups")]
        public virtual School School { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("ClassGroups")]
        public virtual Subject Subject { get; set; }
        [InverseProperty("ClassGroup")]
        public virtual ICollection<ClassClassGroupMap> ClassClassGroupMaps { get; set; }
        [InverseProperty("ClassGroup")]
        public virtual ICollection<ClassGroupTeacherMap> ClassGroupTeacherMaps { get; set; }
    }
}
