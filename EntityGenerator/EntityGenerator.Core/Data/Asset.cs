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
            AssetInventories = new HashSet<AssetInventory>();
            AssetInventoryTransactions = new HashSet<AssetInventoryTransaction>();
            AssetProductMaps = new HashSet<AssetProductMap>();
            AssetSerialMaps = new HashSet<AssetSerialMap>();
            AssetTransactionDetails = new HashSet<AssetTransactionDetail>();
            AssetTransactionSerialMaps = new HashSet<AssetTransactionSerialMap>();
        }

        [Key]
        public long AssetIID { get; set; }
        public long? AssetCategoryID { get; set; }
        [StringLength(50)]
        public string AssetCode { get; set; }
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
        public int? AssetTypeID { get; set; }
        public int? AssetGroupID { get; set; }
        public long? AssetSubCategoryID { get; set; }
        [StringLength(100)]
        public string AssetPrefix { get; set; }
        public long? LastSequenceNumber { get; set; }
        public bool? IsRequiredSerialNumber { get; set; }
        public long? UnitID { get; set; }
        public string Remarks { get; set; }
        [StringLength(100)]
        public string Reference { get; set; }
        public int? DepreciationTypeID { get; set; }

        [ForeignKey("AccumulatedDepGLAccID")]
        [InverseProperty("AssetAccumulatedDepGLAccs")]
        public virtual Account AccumulatedDepGLAcc { get; set; }
        [ForeignKey("AssetCategoryID")]
        [InverseProperty("AssetAssetCategories")]
        public virtual AssetCategory AssetCategory { get; set; }
        [ForeignKey("AssetGlAccID")]
        [InverseProperty("AssetAssetGlAccs")]
        public virtual Account AssetGlAcc { get; set; }
        [ForeignKey("AssetGroupID")]
        [InverseProperty("Assets")]
        public virtual AssetGroup AssetGroup { get; set; }
        [ForeignKey("AssetSubCategoryID")]
        [InverseProperty("AssetAssetSubCategories")]
        public virtual AssetCategory AssetSubCategory { get; set; }
        [ForeignKey("AssetTypeID")]
        [InverseProperty("Assets")]
        public virtual AssetType AssetType { get; set; }
        [ForeignKey("DepreciationExpGLAccId")]
        [InverseProperty("AssetDepreciationExpGLAccs")]
        public virtual Account DepreciationExpGLAcc { get; set; }
        [ForeignKey("DepreciationTypeID")]
        [InverseProperty("Assets")]
        public virtual DepreciationType DepreciationType { get; set; }
        [ForeignKey("UnitID")]
        [InverseProperty("Assets")]
        public virtual Unit Unit { get; set; }
        [InverseProperty("Asset")]
        public virtual ICollection<AssetInventory> AssetInventories { get; set; }
        [InverseProperty("Asset")]
        public virtual ICollection<AssetInventoryTransaction> AssetInventoryTransactions { get; set; }
        [InverseProperty("Asset")]
        public virtual ICollection<AssetProductMap> AssetProductMaps { get; set; }
        [InverseProperty("Asset")]
        public virtual ICollection<AssetSerialMap> AssetSerialMaps { get; set; }
        [InverseProperty("Asset")]
        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }
        [InverseProperty("Asset")]
        public virtual ICollection<AssetTransactionSerialMap> AssetTransactionSerialMaps { get; set; }
    }
}
