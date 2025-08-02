using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeDueMonthlySplit", Schema = "schools")]
    [Index("FeeDueFeeTypeMapsID", Name = "IDX_FeeDueMonthlySplit_FeeDueFeeTypeMapsID")]
    public partial class FeeDueMonthlySplit
    {
        public FeeDueMonthlySplit()
        {
            CampusTransferMonthlySplits = new HashSet<CampusTransferMonthlySplit>();
            CreditNoteFeeTypeMaps = new HashSet<CreditNoteFeeTypeMap>();
            FeeCollectionMonthlySplits = new HashSet<FeeCollectionMonthlySplit>();
            FinalSettlementMonthlySplits = new HashSet<FinalSettlementMonthlySplit>();
            RefundMonthlySplits = new HashSet<RefundMonthlySplit>();
        }

        [Key]
        public long FeeDueMonthlySplitIID { get; set; }
        public long FeeDueFeeTypeMapsID { get; set; }
        public int MonthID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "numeric(3, 0)")]
        public decimal? TaxPercentage { get; set; }
        [Column(TypeName = "numeric(18, 3)")]
        public decimal? TaxAmount { get; set; }
        public int? FeePeriodID { get; set; }
        public int? Year { get; set; }
        public bool Status { get; set; }
        public long? FeeStructureMontlySplitMapID { get; set; }
        public long? AccountTransactionHeadID { get; set; }

        [ForeignKey("AccountTransactionHeadID")]
        [InverseProperty("FeeDueMonthlySplits")]
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
        [ForeignKey("FeeDueFeeTypeMapsID")]
        [InverseProperty("FeeDueMonthlySplits")]
        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMaps { get; set; }
        [ForeignKey("FeePeriodID")]
        [InverseProperty("FeeDueMonthlySplits")]
        public virtual FeePeriod FeePeriod { get; set; }
        [ForeignKey("FeeStructureMontlySplitMapID")]
        [InverseProperty("FeeDueMonthlySplits")]
        public virtual FeeStructureMontlySplitMap FeeStructureMontlySplitMap { get; set; }
        [InverseProperty("FeeDueMonthlySplit")]
        public virtual ICollection<CampusTransferMonthlySplit> CampusTransferMonthlySplits { get; set; }
        [InverseProperty("FeeDueMonthlySplit")]
        public virtual ICollection<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }
        [InverseProperty("FeeDueMonthlySplit")]
        public virtual ICollection<FeeCollectionMonthlySplit> FeeCollectionMonthlySplits { get; set; }
        [InverseProperty("FeeDueMonthlySplit")]
        public virtual ICollection<FinalSettlementMonthlySplit> FinalSettlementMonthlySplits { get; set; }
        [InverseProperty("FeeDueMonthlySplit")]
        public virtual ICollection<RefundMonthlySplit> RefundMonthlySplits { get; set; }
    }
}
