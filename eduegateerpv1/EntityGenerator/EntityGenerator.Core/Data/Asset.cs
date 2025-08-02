using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Assets", Schema = "asset")]
    public partial class Asset
    {
        public Asset()
        {
            AssetTransactionDetails = new HashSet<AssetTransactionDetail>();
        }

        [Key]
        public long AssetIID { get; set; }
        public long? AssetCategoryID { get; set; }
        [StringLength(50)]
        public string AssetCode { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public long? AssetGlAccID { get; set; }
        public long? AccumulatedDepGLAccID { get; set; }
        public long? DepreciationExpGLAccId { get; set; }
        public int? DepreciationYears { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("AccumulatedDepGLAccID")]
        [InverseProperty("AssetAccumulatedDepGLAccs")]
        public virtual Account AccumulatedDepGLAcc { get; set; }
        [ForeignKey("AssetCategoryID")]
        [InverseProperty("Assets")]
        public virtual AssetCategory AssetCategory { get; set; }
        [ForeignKey("AssetGlAccID")]
        [InverseProperty("AssetAssetGlAccs")]
        public virtual Account AssetGlAcc { get; set; }
        [ForeignKey("DepreciationExpGLAccId")]
        [InverseProperty("AssetDepreciationExpGLAccs")]
        public virtual Account DepreciationExpGLAcc { get; set; }
        [InverseProperty("Asset")]
        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }
    }
}
