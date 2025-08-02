using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("GradeMapsForReports", Schema = "schools")]
    public partial class GradeMapsForReport
    {
        [Key]
        public long ReportGradeMapIID { get; set; }
        [StringLength(20)]
        public string GradeName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? GradeFrom { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? GradeTo { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? OutOff { get; set; }
        public bool? IsPercentage { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
    }
}
