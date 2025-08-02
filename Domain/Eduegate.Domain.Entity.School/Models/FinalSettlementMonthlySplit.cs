namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FinalSettlementMonthlySplit", Schema = "schools")]
    public partial class FinalSettlementMonthlySplit
    {
        [Key]
        public long FinalSettlementMonthlySplitIID { get; set; }

        public long FinalSettlementFeeTypeMapId { get; set; }

        public int MonthID { get; set; }

        public decimal? Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxPercentage { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxAmount { get; set; }

        public int? FeePeriodID { get; set; }

        public long? FeeDueMonthlySplitID { get; set; }

        public int Year { get; set; }

        public decimal? CreditNoteAmount { get; set; }

        public decimal? Balance { get; set; }

        public decimal? RefundAmount { get; set; }

        public long? AccountTransactionHeadID { get; set; }

        public decimal? DueAmount { get; set; }

        public decimal? Receivable { get; set; }

        public decimal? NowPaying { get; set; }

        public virtual AccountTransactionHead AccountTransactionHead { get; set; }

        public virtual FeeDueMonthlySplit FeeDueMonthlySplit { get; set; }

        public virtual FinalSettlementFeeTypeMap FinalSettlementFeeTypeMap { get; set; }
    }
}
