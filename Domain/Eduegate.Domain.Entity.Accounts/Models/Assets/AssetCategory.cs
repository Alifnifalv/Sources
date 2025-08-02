using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Assets
{
    [Table("AssetCategories", Schema = "asset")]
    public partial class AssetCategory
    {
        public AssetCategory()
        {
            AssetAssetCategories = new HashSet<Asset>();
            AssetAssetSubCategories = new HashSet<Asset>();
        }

        [Key]
        public long AssetCategoryID { get; set; }

        [StringLength(255)]
        public string CategoryName { get; set; }

        public decimal? DepreciationRate { get; set; }

        public int? DepreciationPeriodID { get; set; }

        [StringLength(100)]
        public string CategoryPrefix { get; set; }

        public long? LastSequenceNumber { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual DepreciationPeriod DepreciationPeriod { get; set; }

        public virtual ICollection<Asset> AssetAssetCategories { get; set; }

        public virtual ICollection<Asset> AssetAssetSubCategories { get; set; }
    }
}