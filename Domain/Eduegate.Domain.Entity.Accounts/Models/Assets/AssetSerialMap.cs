using Eduegate.Domain.Entity.Accounts.Models.Mutual;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Assets
{
    [Table("AssetSerialMaps", Schema = "asset")]
    public partial class AssetSerialMap
    {
        public AssetSerialMap()
        {
            AssetInventoryTransactions = new HashSet<AssetInventoryTransaction>();
        }

        [Key]
        public long AssetSerialMapIID { get; set; }

        public long? AssetID { get; set; }

        public long? AssetInventoryID { get; set; }

        [StringLength(200)]
        public string AssetSequenceCode { get; set; }

        [StringLength(200)]
        public string SerialNumber { get; set; }

        [StringLength(200)]
        public string AssetTag { get; set; }

        public decimal? CostPrice { get; set; }

        public int? ExpectedLife { get; set; }

        public decimal? DepreciationRate { get; set; }

        public decimal? ExpectedScrapValue { get; set; }

        public decimal? AccumulatedDepreciationAmount { get; set; }

        public DateTime? DateOfFirstUse { get; set; }

        public long? SupplierID { get; set; }

        [StringLength(50)]
        public string BillNumber { get; set; }

        public DateTime? BillDate { get; set; }

        public bool? IsActive { get; set; }

        public decimal? LastDepreciationValue { get; set; }

        public decimal? LastDepreciationBooked { get; set; }

        public decimal? LastNetValue { get; set; }

        public DateTime? LastFromDate { get; set; }

        public DateTime? LastToDate { get; set; }

        public DateTime? CutOffDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool? IsDisposed { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual AssetInventory AssetInventory { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<AssetInventoryTransaction> AssetInventoryTransactions { get; set; }
    }
}