using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;

namespace Eduegate.Domain.Entity.Accounts.Models.Assets
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

        public decimal? CostAmount { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? OriginalQty { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<AssetSerialMap> AssetSerialMaps { get; set; }
    }
}