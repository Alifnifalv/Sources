namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.FeeDueMonthlySplit")]
    public partial class FeeDueMonthlySplit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeeDueMonthlySplit()
        {
            CreditNoteFeeTypeMaps = new HashSet<CreditNoteFeeTypeMap>();
            FeeCollectionMonthlySplits = new HashSet<FeeCollectionMonthlySplit>();
            FinalSettlementMonthlySplits = new HashSet<FinalSettlementMonthlySplit>();
            RefundMonthlySplits = new HashSet<RefundMonthlySplit>();
        }

        [Key]
        public long FeeDueMonthlySplitIID { get; set; }

        public long FeeDueFeeTypeMapsID { get; set; }

        public int MonthID { get; set; }

        public decimal? Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxPercentage { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxAmount { get; set; }

        public int? FeePeriodID { get; set; }

        public int? Year { get; set; }

        public bool Status { get; set; }

        public long? FeeStructureMontlySplitMapID { get; set; }

        public long? AccountTransactionHeadID { get; set; }

        public virtual AccountTransactionHead AccountTransactionHead { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeCollectionMonthlySplit> FeeCollectionMonthlySplits { get; set; }

        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMap { get; set; }

        public virtual FeePeriod FeePeriod { get; set; }

        public virtual FeeStructureMontlySplitMap FeeStructureMontlySplitMap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinalSettlementMonthlySplit> FinalSettlementMonthlySplits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefundMonthlySplit> RefundMonthlySplits { get; set; }
    }
}
