using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PackageConfigFeeStructureMaps", Schema = "schools")]
    public partial class PackageConfigFeeStructureMap
    {
        [Key]
        public long PackageConfigFeeStructureMapIID { get; set; }
        public long? PackageConfigID { get; set; }
        public long? FeeStructureID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("FeeStructureID")]
        [InverseProperty("PackageConfigFeeStructureMaps")]
        public virtual FeeStructure FeeStructure { get; set; }
        [ForeignKey("PackageConfigID")]
        [InverseProperty("PackageConfigFeeStructureMaps")]
        public virtual PackageConfig PackageConfig { get; set; }
    }
}
