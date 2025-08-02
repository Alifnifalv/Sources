using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentPromotionLogs", Schema = "schools")]
    [Index("ShiftFromAcademicYearID", Name = "IDX_StudentPromotionLogs_ShiftFromAcademicYearID_StudentID")]
    [Index("StudentID", Name = "IDX_StudentPromotionLogs_StudentID_CreatedDate")]
    [Index("StudentID", Name = "IDX_StudentPromotionLogs_StudentID_PromotionStatusID")]
    public partial class StudentPromotionLog
    {
        [Key]
        public long StudentPromotionLogIID { get; set; }
        public string Remarks { get; set; }
        public int AcademicYearID { get; set; }
        public int ShiftFromAcademicYearID { get; set; }
        public long StudentID { get; set; }
        [Required]
        public bool? Status { get; set; }
        public int ShiftFromClassID { get; set; }
        public int ShiftFromSectionID { get; set; }
        public int ClassID { get; set; }
        public int SectionID { get; set; }
        public byte? SchoolID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? ShiftFromSchoolID { get; set; }
        public bool? IsPromoted { get; set; }
        public byte? PromotionStatusID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StudentPromotionLogAcademicYears")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("StudentPromotionLogClasses")]
        public virtual Class Class { get; set; }
        [ForeignKey("PromotionStatusID")]
        [InverseProperty("StudentPromotionLogs")]
        public virtual PromotionStatus PromotionStatus { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StudentPromotionLogSchools")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("StudentPromotionLogSections")]
        public virtual Section Section { get; set; }
        [ForeignKey("ShiftFromAcademicYearID")]
        [InverseProperty("StudentPromotionLogShiftFromAcademicYears")]
        public virtual AcademicYear ShiftFromAcademicYear { get; set; }
        [ForeignKey("ShiftFromClassID")]
        [InverseProperty("StudentPromotionLogShiftFromClasses")]
        public virtual Class ShiftFromClass { get; set; }
        [ForeignKey("ShiftFromSchoolID")]
        [InverseProperty("StudentPromotionLogShiftFromSchools")]
        public virtual School ShiftFromSchool { get; set; }
        [ForeignKey("ShiftFromSectionID")]
        [InverseProperty("StudentPromotionLogShiftFromSections")]
        public virtual Section ShiftFromSection { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("StudentPromotionLogs")]
        public virtual Student Student { get; set; }
    }
}
