using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FinalSettlementFeeTypeMaps_20230820
    {
        public long FinalSettlementFeeTypeMapsIID { get; set; }
        public long? FinalSettlementID { get; set; }
        public int? FeeMasterID { get; set; }
        public int? FeePeriodID { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? RefundAmount { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Balance { get; set; }
        public long? AccountTransactionHeadID { get; set; }
        public long? FeeCollectionFeeTypeMapsID { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? DueAmount { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Receivable { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? NowPaying { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? CollectedAmount { get; set; }
    }
}
