using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssetTransactionSerialMaps", Schema = "asset")]
    [Index("TransactionDetailID", Name = "IDX_AssetTransactionSerialMaps_TransactionDetailID")]
    public partial class AssetTransactionSerialMap
    {
        [Key]
        public long AssetTransactionSerialMapIID { get; set; }
        public long? TransactionDetailID { get; set; }
        public long? AssetID { get; set; }
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
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? AssetSerialMapID { get; set; }
        public int? AccountingPeriodDays { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DepAccumulatedTillDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DepFromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DepToDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DepAbovePeriod { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BookedDepreciation { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NetValue { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DisposibleAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DifferenceAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PreviousAcculatedDepreciationAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastDepreciationDate { get; set; }

        [ForeignKey("AssetID")]
        [InverseProperty("AssetTransactionSerialMaps")]
        public virtual Asset Asset { get; set; }
        [ForeignKey("SupplierID")]
        [InverseProperty("AssetTransactionSerialMaps")]
        public virtual Supplier Supplier { get; set; }
        [ForeignKey("TransactionDetailID")]
        [InverseProperty("AssetTransactionSerialMaps")]
        public virtual AssetTransactionDetail TransactionDetail { get; set; }
    }
}
