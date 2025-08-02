using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Assets
{
    [Table("AssetTransactionSerialMaps", Schema = "asset")]
    [Index("SerialNumber", Name = "uc_SerialNumber", IsUnique = true)]
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

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? AssetSerialMapID { get; set; }

        public int? AccountingPeriodDays { get; set; }

        public DateTime? DepAccumulatedTillDate { get; set; }

        public DateTime? DepFromDate { get; set; }

        public DateTime? DepToDate { get; set; }

        public decimal? DepAbovePeriod { get; set; }

        public decimal? BookedDepreciation { get; set; }

        public decimal? NetValue { get; set; }

        public decimal? DisposibleAmount { get; set; }

        public decimal? DifferenceAmount { get; set; }

        public decimal? PreviousAcculatedDepreciationAmount { get; set; }

        public DateTime? LastDepreciationDate { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual AssetTransactionDetail TransactionDetail { get; set; }
    }
}