using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PackageConfig", Schema = "schools")]
    public partial class PackageConfig
    {
        public PackageConfig()
        {
            PackageConfigClassMaps = new HashSet<PackageConfigClassMap>();
            PackageConfigFeeStructureMaps = new HashSet<PackageConfigFeeStructureMap>();
            PackageConfigStudentGroupMaps = new HashSet<PackageConfigStudentGroupMap>();
            PackageConfigStudentMaps = new HashSet<PackageConfigStudentMap>();
        }

        [Key]
        public long PackageConfigIID { get; set; }
        public int? AcadamicYearID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [StringLength(25)]
        public string Name { get; set; }
        public byte? SchoolID { get; set; }
        public bool? IsAutoCreditNote { get; set; }
        public long? CreditNoteAccountID { get; set; }

        [ForeignKey("AcadamicYearID")]
        [InverseProperty("PackageConfigs")]
        public virtual AcademicYear AcadamicYear { get; set; }
        [ForeignKey("CreditNoteAccountID")]
        [InverseProperty("PackageConfigs")]
        public virtual Account CreditNoteAccount { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("PackageConfigs")]
        public virtual School School { get; set; }
        [InverseProperty("PackageConfig")]
        public virtual ICollection<PackageConfigClassMap> PackageConfigClassMaps { get; set; }
        [InverseProperty("PackageConfig")]
        public virtual ICollection<PackageConfigFeeStructureMap> PackageConfigFeeStructureMaps { get; set; }
        [InverseProperty("PackageConfig")]
        public virtual ICollection<PackageConfigStudentGroupMap> PackageConfigStudentGroupMaps { get; set; }
        [InverseProperty("PackageConfig")]
        public virtual ICollection<PackageConfigStudentMap> PackageConfigStudentMaps { get; set; }
    }
}
