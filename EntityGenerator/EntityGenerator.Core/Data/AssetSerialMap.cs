using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CostPrice { get; set; }
        public int? ExpectedLife { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DepreciationRate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ExpectedScrapValue { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? AccumulatedDepreciationAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfFirstUse { get; set; }
        public long? SupplierID { get; set; }
        [StringLength(50)]
        public string BillNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BillDate { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LastDepreciationValue { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LastDepreciationBooked { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LastNetValue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastFromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastToDate { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CutOffDate { get; set; }
        public bool? IsDisposed { get; set; }

        [ForeignKey("AssetID")]
        [InverseProperty("AssetSerialMaps")]
        public virtual Asset Asset { get; set; }
        [ForeignKey("AssetInventoryID")]
        [InverseProperty("AssetSerialMaps")]
        public virtual AssetInventory AssetInventory { get; set; }
        [ForeignKey("SupplierID")]
        [InverseProperty("AssetSerialMaps")]
        public virtual Supplier Supplier { get; set; }
        [InverseProperty("AssetSerialMap")]
        public virtual ICollection<AssetInventoryTransaction> AssetInventoryTransactions { get; set; }
    }
}
