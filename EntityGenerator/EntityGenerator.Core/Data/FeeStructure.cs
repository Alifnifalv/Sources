using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeStructures", Schema = "schools")]
    public partial class FeeStructure
    {
        public FeeStructure()
        {
            ClassFeeStructureMaps = new HashSet<ClassFeeStructureMap>();
            FeeStructureFeeMaps = new HashSet<FeeStructureFeeMap>();
            PackageConfigFeeStructureMaps = new HashSet<PackageConfigFeeStructureMap>();
        }

        [Key]
        public long FeeStructureIID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int? AcadamicYearID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }

        [ForeignKey("AcadamicYearID")]
        [InverseProperty("FeeStructures")]
        public virtual AcademicYear AcadamicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("FeeStructures")]
        public virtual School School { get; set; }
        [InverseProperty("FeeStructure")]
        public virtual ICollection<ClassFeeStructureMap> ClassFeeStructureMaps { get; set; }
        [InverseProperty("FeeStructure")]
        public virtual ICollection<FeeStructureFeeMap> FeeStructureFeeMaps { get; set; }
        [InverseProperty("FeeStructure")]
        public virtual ICollection<PackageConfigFeeStructureMap> PackageConfigFeeStructureMaps { get; set; }
    }
}
