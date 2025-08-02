using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;

namespace Eduegate.Domain.Entity.Accounts.Models.Assets
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

        public decimal? Quantity { get; set; }

        public decimal? Amount { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? AccountID { get; set; }

        public long? AssetGlAccID { get; set; }

        public long? AccumulatedDepGLAccID { get; set; }

        public long? DepreciationExpGLAccID { get; set; }

        public decimal? CostAmount { get; set; }

        public int? AccountingPeriodDays { get; set; }

        public DateTime? DepAccumulatedTillDate { get; set; }

        public DateTime? DepFromDate { get; set; }

        public DateTime? DepToDate { get; set; }

        public decimal? DepAbovePeriod { get; set; }

        public decimal? BookedDepreciation { get; set; }

        public DateTime? CutOffDate { get; set; }

        public decimal? NetValue { get; set; }

        public decimal? PreviousAcculatedDepreciationAmount { get; set; }

        public decimal? AccumulatedDepreciationAmount { get; set; }

        public long? ProductSKUMapID { get; set; }

        public virtual Account Account { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual AssetTransactionHead Head { get; set; }

        public virtual ICollection<AssetTransactionSerialMap> AssetTransactionSerialMaps { get; set; }
    }
}