using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssetTransactionDetails", Schema = "asset")]
    public partial class AssetTransactionDetail
    {
        public AssetTransactionDetail()
        {
            AssetTransactionSerialMaps = new HashSet<AssetTransactionSerialMap>();
        }

        [Key]
        public long DetailIID { get; set; }
        public long? HeadID { get; set; }
        public long? AssetID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? AccountID { get; set; }
        public long? AssetGlAccID { get; set; }
        public long? AccumulatedDepGLAccID { get; set; }
        public long? DepreciationExpGLAccID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CostAmount { get; set; }
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
        [Column(TypeName = "datetime")]
        public DateTime? CutOffDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NetValue { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PreviousAcculatedDepreciationAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? AccumulatedDepreciationAmount { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("AssetTransactionDetails")]
        public virtual Account Account { get; set; }
        [ForeignKey("AssetID")]
        [InverseProperty("AssetTransactionDetails")]
        public virtual Asset Asset { get; set; }
        [ForeignKey("HeadID")]
        [InverseProperty("AssetTransactionDetails")]
        public virtual AssetTransactionHead Head { get; set; }
        [InverseProperty("TransactionDetail")]
        public virtual ICollection<AssetTransactionSerialMap> AssetTransactionSerialMaps { get; set; }
    }
}
