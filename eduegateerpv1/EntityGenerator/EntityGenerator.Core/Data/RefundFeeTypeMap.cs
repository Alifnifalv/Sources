using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RefundFeeTypeMaps", Schema = "schools")]
    public partial class RefundFeeTypeMap
    {
        public RefundFeeTypeMap()
        {
            RefundMonthlySplits = new HashSet<RefundMonthlySplit>();
        }

        [Key]
        public long RefundFeeTypeMapsIID { get; set; }
        public long? RefundID { get; set; }
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
        public decimal? NowPaying { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? CollectedAmount { get; set; }

        [ForeignKey("AccountTransactionHeadID")]
        [InverseProperty("RefundFeeTypeMaps")]
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
        [ForeignKey("FeeCollectionFeeTypeMapsID")]
        [InverseProperty("RefundFeeTypeMaps")]
        public virtual FeeCollectionFeeTypeMap FeeCollectionFeeTypeMaps { get; set; }
        [ForeignKey("FeeDueFeeTypeMapsID")]
        [InverseProperty("RefundFeeTypeMaps")]
        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMaps { get; set; }
        [ForeignKey("FeeMasterID")]
        [InverseProperty("RefundFeeTypeMaps")]
        public virtual FeeMaster FeeMaster { get; set; }
        [ForeignKey("FeePeriodID")]
        [InverseProperty("RefundFeeTypeMaps")]
        public virtual FeePeriod FeePeriod { get; set; }
        [ForeignKey("RefundID")]
        [InverseProperty("RefundFeeTypeMaps")]
        public virtual Refund Refund { get; set; }
        [InverseProperty("RefundFeeTypeMap")]
        public virtual ICollection<RefundMonthlySplit> RefundMonthlySplits { get; set; }
    }
}
