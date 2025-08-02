using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AcademicClassMaps", Schema = "schools")]
    public partial class AcademicClassMap
    {
        [Key]
        public long AcademicClassMapIID { get; set; }
        public int? ClassID { get; set; }
        public int? TotalWorkingDays { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public byte? MonthID { get; set; }
        public int? YearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("AcademicClassMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("AcademicClassMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("AcademicClassMaps")]
        public virtual School School { get; set; }
    }
}
