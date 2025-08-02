using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DepreciationRate { get; set; }
        public int? DepreciationPeriodID { get; set; }
        [StringLength(100)]
        public string CategoryPrefix { get; set; }
        public long? LastSequenceNumber { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("DepreciationPeriodID")]
        [InverseProperty("AssetCategories")]
        public virtual DepreciationPeriod DepreciationPeriod { get; set; }
        [InverseProperty("AssetCategory")]
        public virtual ICollection<Asset> AssetAssetCategories { get; set; }
        [InverseProperty("AssetSubCategory")]
        public virtual ICollection<Asset> AssetAssetSubCategories { get; set; }
    }
}
