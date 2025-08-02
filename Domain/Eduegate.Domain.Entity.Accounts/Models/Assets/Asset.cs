using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using Eduegate.Domain.Entity.Accounts.Models.Catalog;

namespace Eduegate.Domain.Entity.Accounts.Models.Assets
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

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

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

        public virtual Account AccumulatedDepGLAcc { get; set; }

        public virtual AssetCategory AssetCategory { get; set; }

        public virtual Account AssetGlAcc { get; set; }

        public virtual AssetGroup AssetGroup { get; set; }

        public virtual AssetCategory AssetSubCategory { get; set; }

        public virtual AssetType AssetType { get; set; }

        public virtual Account DepreciationExpGLAcc { get; set; }

        public virtual DepreciationType DepreciationType { get; set; }

        public virtual Unit Unit { get; set; }

        public virtual ICollection<AssetInventory> AssetInventories { get; set; }

        public virtual ICollection<AssetInventoryTransaction> AssetInventoryTransactions { get; set; }

        public virtual ICollection<AssetProductMap> AssetProductMaps { get; set; }

        public virtual ICollection<AssetSerialMap> AssetSerialMaps { get; set; }

        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }

        public virtual ICollection<AssetTransactionSerialMap> AssetTransactionSerialMaps { get; set; }
    }
}