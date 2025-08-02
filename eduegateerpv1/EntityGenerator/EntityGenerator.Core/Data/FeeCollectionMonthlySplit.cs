using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeCollectionMonthlySplit", Schema = "schools")]
    [Index("MonthID", "FeeDueMonthlySplitID", "Year", Name = "FeeCollectionMonthlySplit_MonthID_FeeDueMonthlySplitID_Year")]
    [Index("FeeCollectionFeeTypeMapId", Name = "idx_FeeCollectionMonthlySplitFeeCollectionFeeTypeMapId")]
    [Index("FeeDueMonthlySplitID", Name = "idx_FeeCollectionMonthlySplitFeeDueMonthlySplitID")]
    public partial class FeeCollectionMonthlySplit
    {
        [Key]
        public long FeeCollectionMonthlySplitIID { get; set; }
        public long FeeCollectionFeeTypeMapId { get; set; }
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

        [ForeignKey("AccountTransactionHeadID")]
        [InverseProperty("FeeCollectionMonthlySplits")]
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
        [ForeignKey("FeeCollectionFeeTypeMapId")]
        [InverseProperty("FeeCollectionMonthlySplits")]
        public virtual FeeCollectionFeeTypeMap FeeCollectionFeeTypeMap { get; set; }
        [ForeignKey("FeeDueMonthlySplitID")]
        [InverseProperty("FeeCollectionMonthlySplits")]
        public virtual FeeDueMonthlySplit FeeDueMonthlySplit { get; set; }
    }
}
