namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.GradeMapsForReports")]
    public partial class GradeMapsForReport
    {
        [Key]
        public long ReportGradeMapIID { get; set; }

        [StringLength(20)]
        public string GradeName { get; set; }

        public decimal? GradeFrom { get; set; }

        public decimal? GradeTo { get; set; }

        public decimal? OutOff { get; set; }

        public bool? IsPercentage { get; set; }

        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }
    }
}
