using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FinalSettlementMonthlySplit", Schema = "schools")]
    public partial class FinalSettlementMonthlySplit
    {
        [Key]
        public long FinalSettlementMonthlySplitIID { get; set; }
        public long FinalSettlementFeeTypeMapId { get; set; }
        public int MonthID { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "numeric(3, 0)")]
        public decimal? TaxPercentage { get; set; }
        [Column(TypeName = "numeric(18, 3)")]
        public decimal? TaxAmount { get; set; }
        public int? FeePeriodID { get; set; }
        public long? FeeDueMonthlySplitID { get; set; }
        public int Year { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? CreditNoteAmount { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Balance { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? RefundAmount { get; set; }
        public long? AccountTransactionHeadID { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? DueAmount { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Receivable { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? NowPaying { get; set; }

        [ForeignKey("AccountTransactionHeadID")]
        [InverseProperty("FinalSettlementMonthlySplits")]
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
        [ForeignKey("FeeDueMonthlySplitID")]
        [InverseProperty("FinalSettlementMonthlySplits")]
        public virtual FeeDueMonthlySplit FeeDueMonthlySplit { get; set; }
        [ForeignKey("FinalSettlementFeeTypeMapId")]
        [InverseProperty("FinalSettlementMonthlySplits")]
        public virtual FinalSettlementFeeTypeMap FinalSettlementFeeTypeMap { get; set; }
    }
}
