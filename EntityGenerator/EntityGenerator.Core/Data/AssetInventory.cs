using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssetInventories", Schema = "asset")]
    public partial class AssetInventory
    {
        public AssetInventory()
        {
            AssetSerialMaps = new HashSet<AssetSerialMap>();
        }

        [Key]
        public long AssetInventoryIID { get; set; }
        public long? AssetID { get; set; }
        public long? BranchID { get; set; }
        public long? Batch { get; set; }
        public long? HeadID { get; set; }
        public int? CompanyID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CostAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? OriginalQty { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("AssetID")]
        [InverseProperty("AssetInventories")]
        public virtual Asset Asset { get; set; }
        [ForeignKey("BranchID")]
        [InverseProperty("AssetInventories")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("CompanyID")]
        [InverseProperty("AssetInventories")]
        public virtual Company Company { get; set; }
        [InverseProperty("AssetInventory")]
        public virtual ICollection<AssetSerialMap> AssetSerialMaps { get; set; }
    }
}
