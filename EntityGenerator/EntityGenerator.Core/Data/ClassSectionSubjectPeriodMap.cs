using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassSectionSubjectPeriodMaps", Schema = "schools")]
    public partial class ClassSectionSubjectPeriodMap
    {
        [Key]
        public long PeriodMapIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? SubjectTypeID { get; set; }
        public int? SubjectID { get; set; }
        public int? TotalPeriods { get; set; }
        public int? WeekPeriods { get; set; }
        public int? MinimumPeriods { get; set; }
        public int? MaximumPeriods { get; set; }
        public int? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Required]
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ClassSectionSubjectPeriodMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("ClassSectionSubjectPeriodMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("ClassSectionSubjectPeriodMaps")]
        public virtual Section Section { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("ClassSectionSubjectPeriodMaps")]
        public virtual Subject Subject { get; set; }
    }
}
