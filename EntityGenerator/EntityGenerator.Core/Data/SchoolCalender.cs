using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SchoolCalenders", Schema = "schools")]
    public partial class SchoolCalender
    {
        [Key]
        public int SchoolCalenderID { get; set; }
        [StringLength(50)]
        public string CalenderName { get; set; }
        [StringLength(500)]
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

        [ForeignKey("AcademicYearID")]
        [InverseProperty("SchoolCalenders")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("SchoolCalenders")]
        public virtual School School { get; set; }
    }
}
