using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WPSHead", Schema = "schools")]
    public partial class WPSHead
    {
        public WPSHead()
        {
            WPSDetail1 = new HashSet<WPSDetail1>();
        }

        [Key]
        public long HeadIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FileCreationDate { get; set; }
        public TimeSpan? FileCreationTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SalaryYearAndMonth { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalSalaries { get; set; }
        public int? TotalRecords { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("WPSHeads")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("WPSHeads")]
        public virtual School School { get; set; }
        [InverseProperty("HeadI")]
        public virtual ICollection<WPSDetail1> WPSDetail1 { get; set; }
    }
}
