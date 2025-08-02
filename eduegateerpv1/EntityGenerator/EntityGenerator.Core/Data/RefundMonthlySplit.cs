using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RefundMonthlySplit", Schema = "schools")]
    public partial class RefundMonthlySplit
    {
        [Key]
        public long RefundMonthlySplitIID { get; set; }
        public long RefundFeeTypeMapId { get; set; }
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
        public decimal? NowPaying { get; set; }

        [ForeignKey("AccountTransactionHeadID")]
        [InverseProperty("RefundMonthlySplits")]
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
        [ForeignKey("FeeDueMonthlySplitID")]
        [InverseProperty("RefundMonthlySplits")]
        public virtual FeeDueMonthlySplit FeeDueMonthlySplit { get; set; }
        [ForeignKey("RefundFeeTypeMapId")]
        [InverseProperty("RefundMonthlySplits")]
        public virtual RefundFeeTypeMap RefundFeeTypeMap { get; set; }
    }
}
