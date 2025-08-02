using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FinalSettlementFeeTypeMaps", Schema = "schools")]
    [Index("FeeCollectionFeeTypeMapsID", Name = "IDX_FinalSettlementFeeTypeMaps_FeeCollectionFeeTypeMapsID_")]
    [Index("FeeDueFeeTypeMapsID", Name = "IDX_FinalSettlementFeeTypeMaps_FeeDueFeeTypeMapsID_")]
    [Index("FeeDueFeeTypeMapsID", Name = "IDX_FinalSettlementFeeTypeMaps_FeeDueFeeTypeMapsID_FinalSettlementID__FeeMasterID__RefundAmount__Re")]
    [Index("FeeMasterID", Name = "IDX_FinalSettlementFeeTypeMaps_FeeMasterID_FinalSettlementID__FeeDueFeeTypeMapsID__RefundAmount__Re")]
    [Index("FeeMasterID", "FeeDueFeeTypeMapsID", Name = "IDX_FinalSettlementFeeTypeMaps_FeeMasterID__FeeDueFeeTypeMapsID_")]
    [Index("FinalSettlementID", Name = "IDX_FinalSettlementFeeTypeMaps_FinalSettlementID_")]
    [Index("FinalSettlementID", Name = "IDX_FinalSettlementFeeTypeMaps_FinalSettlementID_FeeMasterID__FeeDueFeeTypeMapsID__RefundAmount__Re")]
    [Index("FinalSettlementID", Name = "IDX_FinalSettlementFeeTypeMaps_FinalSettlementID_FeeMasterID__FeePeriodID__FeeDueFeeTypeMapsID__Ref")]
    public partial class FinalSettlementFeeTypeMap
    {
        public FinalSettlementFeeTypeMap()
        {
            FinalSettlementMonthlySplits = new HashSet<FinalSettlementMonthlySplit>();
        }

        [Key]
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

        [ForeignKey("AccountTransactionHeadID")]
        [InverseProperty("FinalSettlementFeeTypeMaps")]
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
        [ForeignKey("FeeCollectionFeeTypeMapsID")]
        [InverseProperty("FinalSettlementFeeTypeMaps")]
        public virtual FeeCollectionFeeTypeMap FeeCollectionFeeTypeMaps { get; set; }
        [ForeignKey("FeeDueFeeTypeMapsID")]
        [InverseProperty("FinalSettlementFeeTypeMaps")]
        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMaps { get; set; }
        [ForeignKey("FeeMasterID")]
        [InverseProperty("FinalSettlementFeeTypeMaps")]
        public virtual FeeMaster FeeMaster { get; set; }
        [ForeignKey("FeePeriodID")]
        [InverseProperty("FinalSettlementFeeTypeMaps")]
        public virtual FeePeriod FeePeriod { get; set; }
        [ForeignKey("FinalSettlementID")]
        [InverseProperty("FinalSettlementFeeTypeMaps")]
        public virtual FinalSettlement FinalSettlement { get; set; }
        [InverseProperty("FinalSettlementFeeTypeMap")]
        public virtual ICollection<FinalSettlementMonthlySplit> FinalSettlementMonthlySplits { get; set; }
    }
}
