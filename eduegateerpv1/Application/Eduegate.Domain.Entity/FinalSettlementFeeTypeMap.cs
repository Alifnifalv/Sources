namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.FinalSettlementFeeTypeMaps")]
    public partial class FinalSettlementFeeTypeMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FinalSettlementFeeTypeMap()
        {
            FinalSettlementMonthlySplits = new HashSet<FinalSettlementMonthlySplit>();
        }

        [Key]
        public long FinalSettlementFeeTypeMapsIID { get; set; }

        public long? FinalSettlementID { get; set; }

        public int? FeeMasterID { get; set; }

        public int? FeePeriodID { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? FeeDueFeeTypeMapsID { get; set; }

        public decimal? RefundAmount { get; set; }

        public decimal? Balance { get; set; }

        public long? AccountTransactionHeadID { get; set; }

        public long? FeeCollectionFeeTypeMapsID { get; set; }

        public decimal? DueAmount { get; set; }

        public decimal? Receivable { get; set; }

        public decimal? NowPaying { get; set; }

        public decimal? CollectedAmount { get; set; }

        public virtual AccountTransactionHead AccountTransactionHead { get; set; }

        public virtual FeeCollectionFeeTypeMap FeeCollectionFeeTypeMap { get; set; }

        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMap { get; set; }

        public virtual FeeMaster FeeMaster { get; set; }

        public virtual FeePeriod FeePeriod { get; set; }

        public virtual FinalSettlement FinalSettlement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinalSettlementMonthlySplit> FinalSettlementMonthlySplits { get; set; }
    }
}
