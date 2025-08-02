using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class PackageConfigSearch
    {
        public long PackageConfigIID { get; set; }
        [StringLength(25)]
        public string Package { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [StringLength(126)]
        public string AcademicYear { get; set; }
        public int? AcadamicYearID { get; set; }
        public byte? SchoolID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [Required]
        [StringLength(4000)]
        public string FeeStructure { get; set; }
        [Required]
        [StringLength(4000)]
        public string Class { get; set; }
        [Required]
        [StringLength(4000)]
        public string StudentGroup { get; set; }
        [Required]
        [StringLength(4000)]
        public string Student { get; set; }
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
