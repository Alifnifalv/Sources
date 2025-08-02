using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MarkRegisters", Schema = "schools")]
    [Index("AcademicYearID", Name = "IDX_MarkRegisters_AcademicYearID")]
    [Index("AcademicYearID", Name = "IDX_MarkRegisters_AcademicYearID_ExamID__CreatedBy__UpdatedBy__CreatedDate__UpdatedDate__TimeStamps")]
    [Index("AcademicYearID", "PresentStatusID", Name = "IDX_MarkRegisters_AcademicYearID__PresentStatusID_ExamID__StudentId__ClassID__SectionID__SchoolID__")]
    [Index("ClassID", "AcademicYearID", Name = "IDX_MarkRegisters_ClassID__AcademicYearID_StudentId__SectionID__SchoolID")]
    [Index("ExamGroupID", Name = "IDX_MarkRegisters_ExamGroupID_ExamID__CreatedBy__UpdatedBy__CreatedDate__UpdatedDate__TimeStamps__S")]
    [Index("ExamID", Name = "IDX_MarkRegisters_ExamID_CreatedBy__UpdatedBy__CreatedDate__UpdatedDate__TimeStamps__StudentId__Cla")]
    [Index("SchoolID", "AcademicYearID", Name = "IDX_MarkRegisters_ExamID_StudentId_ClassID_SectionID")]
    [Index("ExamID", "StudentId", "ClassID", "SectionID", "AcademicYearID", Name = "IDX_MarkRegisters_ExamID_StudentId_ClassID_SectionID_AcademicYearID")]
    [Index("ExamID", "ClassID", "AcademicYearID", "ExamGroupID", Name = "IDX_MarkRegisters_ExamID__ClassID__AcademicYearID__ExamGroupID_StudentId__PresentStatusID")]
    [Index("PresentStatusID", Name = "IDX_MarkRegisters_PresentStatusID_ExamID__StudentId__ClassID__SchoolID__AcademicYearID__MarkEntrySt")]
    [Index("PresentStatusID", Name = "IDX_MarkRegisters_PresentStatusID_ExamID__StudentId__ClassID__SectionID__SchoolID__AcademicYearID__")]
    [Index("SchoolID", Name = "IDX_MarkRegisters_SchoolID_ExamID__StudentId__ClassID__SectionID__AcademicYearID__ExamGroupID__Pres")]
    [Index("StudentId", "SchoolID", "AcademicYearID", Name = "idx_MarkRegisters_StudentId_SchoolID_AcademicYearID")]
    public partial class MarkRegister
    {
        public MarkRegister()
        {
            MarkRegisterSkillGroups = new HashSet<MarkRegisterSkillGroup>();
            MarkRegisterSubjectMaps = new HashSet<MarkRegisterSubjectMap>();
        }

        [Key]
        public long MarkRegisterIID { get; set; }
        public long? ExamID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? StudentId { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? MarkEntryStatusID { get; set; }
        public int? ExamGroupID { get; set; }
        public byte? PresentStatusID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("MarkRegisters")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("MarkRegisters")]
        public virtual Class Class { get; set; }
        [ForeignKey("ExamID")]
        [InverseProperty("MarkRegisters")]
        public virtual Exam Exam { get; set; }
        [ForeignKey("ExamGroupID")]
        [InverseProperty("MarkRegisters")]
        public virtual ExamGroup ExamGroup { get; set; }
        [ForeignKey("MarkEntryStatusID")]
        [InverseProperty("MarkRegisters")]
        public virtual MarkEntryStatus MarkEntryStatus { get; set; }
        [ForeignKey("PresentStatusID")]
        [InverseProperty("MarkRegisters")]
        public virtual PresentStatus PresentStatus { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("MarkRegisters")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("MarkRegisters")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentId")]
        [InverseProperty("MarkRegisters")]
        public virtual Student Student { get; set; }
        [InverseProperty("MarkRegister")]
        public virtual ICollection<MarkRegisterSkillGroup> MarkRegisterSkillGroups { get; set; }
        [InverseProperty("MarkRegister")]
        public virtual ICollection<MarkRegisterSubjectMap> MarkRegisterSubjectMaps { get; set; }
    }
}
