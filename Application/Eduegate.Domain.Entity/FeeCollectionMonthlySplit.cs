namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.FeeCollectionMonthlySplit")]
    public partial class FeeCollectionMonthlySplit
    {
        [Key]
        public long FeeCollectionMonthlySplitIID { get; set; }

        public long FeeCollectionFeeTypeMapId { get; set; }

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

        public virtual AccountTransactionHead AccountTransactionHead { get; set; }

        public virtual FeeCollectionFeeTypeMap FeeCollectionFeeTypeMap { get; set; }

        public virtual FeeDueMonthlySplit FeeDueMonthlySplit { get; set; }
    }
}
