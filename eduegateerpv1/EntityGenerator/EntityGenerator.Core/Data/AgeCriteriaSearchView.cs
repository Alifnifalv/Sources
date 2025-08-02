using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AgeCriteriaSearchView
    {
        public long AgeCriteriaIID { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public byte? CurriculumID { get; set; }
        [StringLength(50)]
        public string SyllabusDescription { get; set; }
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
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
    }
}
