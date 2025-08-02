using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassSubjectMaps", Schema = "schools")]
    [Index("ClassID", "AcademicYearID", Name = "IDX_ClassSubjectMaps_ClassID_AcademicYearID")]
    [Index("ClassID", "SchoolID", "AcademicYearID", Name = "IDX_ClassSubjectMaps_ClassID_SchoolID_AcademicYearID")]
    [Index("ClassID", "SectionID", "AcademicYearID", Name = "IDX_ClassSubjectMaps_ClassID__SectionID__AcademicYearID_SubjectID__EmployeeID__CreatedBy__UpdatedBy")]
    public partial class ClassSubjectMap
    {
        public ClassSubjectMap()
        {
            ClassSubjectWorkflowEntityMaps = new HashSet<ClassSubjectWorkflowEntityMap>();
        }

        [Key]
        public long ClassSubjectMapIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? SubjectID { get; set; }
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
        public int? WeekPeriods { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ClassSubjectMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("ClassSubjectMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("ClassSubjectMaps")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ClassSubjectMaps")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("ClassSubjectMaps")]
        public virtual Section Section { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("ClassSubjectMaps")]
        public virtual Subject Subject { get; set; }
        [InverseProperty("ClassSubjectMap")]
        public virtual ICollection<ClassSubjectWorkflowEntityMap> ClassSubjectWorkflowEntityMaps { get; set; }
    }
}
