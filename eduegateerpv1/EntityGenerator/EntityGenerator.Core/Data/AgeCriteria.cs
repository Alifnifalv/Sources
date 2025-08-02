using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AgeCriteria", Schema = "schools")]
    public partial class AgeCriteria
    {
        [Key]
        public long AgeCriteriaIID { get; set; }
        public int? ClassID { get; set; }
        public byte? CurriculumID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MinAge { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MaxAge { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BirthFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BirthTo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("AgeCriterias")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("AgeCriterias")]
        public virtual Class Class { get; set; }
        [ForeignKey("CurriculumID")]
        [InverseProperty("AgeCriterias")]
        public virtual Syllabu Curriculum { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("AgeCriterias")]
        public virtual School School { get; set; }
    }
}
