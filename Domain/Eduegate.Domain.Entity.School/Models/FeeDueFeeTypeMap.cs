namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("FeeDueFeeTypeMaps", Schema = "schools")]
    public partial class FeeDueFeeTypeMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeeDueFeeTypeMap()
        {
            CreditNoteFeeTypeMaps = new HashSet<CreditNoteFeeTypeMap>();
            FeeCollectionFeeTypeMaps = new HashSet<FeeCollectionFeeTypeMap>();
            FeeDueMonthlySplits = new HashSet<FeeDueMonthlySplit>();
            FinalSettlementFeeTypeMaps = new HashSet<FinalSettlementFeeTypeMap>();
            RefundFeeTypeMaps = new HashSet<RefundFeeTypeMap>();
            FeeDueCancellations = new HashSet<FeeDueCancellation>();
            CampusTransferFeeTypeMaps = new HashSet<CampusTransferFeeTypeMap>();
        }

        [Key]
        public long FeeDueFeeTypeMapsIID { get; set; }

        public long? StudentFeeDueID { get; set; }

        public long? ClassFeeMasterID { get; set; }

        public int? FeePeriodID { get; set; }

        public decimal? Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxPercentage { get; set; }

        public decimal? TaxAmount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public bool Status { get; set; }

        public int? FeeMasterID { get; set; }

        public long? FeeMasterClassMapID { get; set; }

        public long? FineMasterStudentMapID { get; set; }

        public int? FineMasterID { get; set; }

        public decimal? CollectedAmount { get; set; }

        public long? FeeStructureFeeMapID { get; set; }

        public virtual ClassFeeMaster ClassFeeMaster { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeCollectionFeeTypeMap> FeeCollectionFeeTypeMaps { get; set; }

        public virtual FeePeriod FeePeriod { get; set; }

        public virtual FeeStructureFeeMap FeeStructureFeeMap { get; set; }

        public virtual FineMaster FineMaster { get; set; }

        public virtual FineMasterStudentMap FineMasterStudentMap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueMonthlySplit> FeeDueMonthlySplits { get; set; }

        public virtual FeeMaster FeeMaster { get; set; }

        public virtual FeeMasterClassMap FeeMasterClassMap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinalSettlementFeeTypeMap> FinalSettlementFeeTypeMaps { get; set; }

        public virtual StudentFeeDue StudentFeeDue { get; set; }

        public long? AccountTransactionHeadID { get; set; }

        public virtual AccountTransactionHead AccountTransactionHead { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefundFeeTypeMap> RefundFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueCancellation> FeeDueCancellations { get; set; }

        public virtual ICollection<CampusTransferFeeTypeMap> CampusTransferFeeTypeMaps { get; set; }
    }
}
