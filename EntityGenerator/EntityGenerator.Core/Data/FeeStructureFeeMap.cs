using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeStructureFeeMaps", Schema = "schools")]
    [Index("FeePeriodID", Name = "IDX_FeeStructureFeeMaps_FeePeriod")]
    [Index("FeeStructureID", Name = "IDX_FeeStructureFeeMaps_FeeStructureID")]
    public partial class FeeStructureFeeMap
    {
        public FeeStructureFeeMap()
        {
            FeeDueFeeTypeMaps = new HashSet<FeeDueFeeTypeMap>();
            FeeStructureMontlySplitMaps = new HashSet<FeeStructureMontlySplitMap>();
        }

        [Key]
        public long FeeStructureFeeMapIID { get; set; }
        public long FeeStructureID { get; set; }
        public int? FeeMasterID { get; set; }
        public int? FeePeriodID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("FeeMasterID")]
        [InverseProperty("FeeStructureFeeMaps")]
        public virtual FeeMaster FeeMaster { get; set; }
        [ForeignKey("FeePeriodID")]
        [InverseProperty("FeeStructureFeeMaps")]
        public virtual FeePeriod FeePeriod { get; set; }
        [ForeignKey("FeeStructureID")]
        [InverseProperty("FeeStructureFeeMaps")]
        public virtual FeeStructure FeeStructure { get; set; }
        [InverseProperty("FeeStructureFeeMap")]
        public virtual ICollection<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }
        [InverseProperty("FeeStructureFeeMap")]
        public virtual ICollection<FeeStructureMontlySplitMap> FeeStructureMontlySplitMaps { get; set; }
    }
}
