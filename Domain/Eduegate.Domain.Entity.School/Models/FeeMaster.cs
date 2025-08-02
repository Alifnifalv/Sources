namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FeeMasters", Schema = "schools")]
    public partial class FeeMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeeMaster()
        {
            CreditNoteFeeTypeMaps = new HashSet<CreditNoteFeeTypeMap>();
            FeeCollectionFeeTypeMaps = new HashSet<FeeCollectionFeeTypeMap>();
            FeeCollections = new HashSet<FeeCollection>();
            FeeDueFeeTypeMaps = new HashSet<FeeDueFeeTypeMap>();
            FeeMasterClassMaps = new HashSet<FeeMasterClassMap>();
            FeeStructureFeeMaps = new HashSet<FeeStructureFeeMap>();
            FinalSettlementFeeTypeMaps = new HashSet<FinalSettlementFeeTypeMap>();
            StudentFeeConcessions = new HashSet<StudentFeeConcession>();
            StudentGroupFeeTypeMaps = new HashSet<StudentGroupFeeTypeMap>();
            RefundFeeTypeMaps = new HashSet<RefundFeeTypeMap>();
            CampusTransferFeeTypeMaps = new HashSet<CampusTransferFeeTypeMap>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeeMasterID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public int? FeeTypeID { get; set; }

        public DateTime? DueDate { get; set; }

        public decimal? Amount { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public byte? FeeCycleID { get; set; }

        public long? LedgerAccountID { get; set; }

        public long? TaxLedgerAccountID { get; set; }

        public decimal? TaxPercentage { get; set; }

        public decimal? OSTaxPercentage { get; set; }

        public decimal? AdvanceTaxPercentage { get; set; }

        public int? DueInDays { get; set; }
        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public long? AdvanceAccountID { get; set; }

        public long? OutstandingAccountID { get; set; }

        public long? ProvisionforAdvanceAccountID { get; set; }

        public long? ProvisionforOutstandingAccountID { get; set; }

        public long? OSTaxAccountID { get; set; }

        public long? AdvanceTaxAccountID { get; set; }

        public bool? IsExternal { get; set; }

        public bool? IsActive { get; set; }

        public string ReportName { get; set; }
        public virtual AcademicYear AcademicYear { get; set; }
        public virtual Schools School { get; set; }

        public virtual Account Account { get; set; }

        public virtual Account Account1 { get; set; }

        public virtual Account Account2 { get; set; }

        public virtual Account Account3 { get; set; }

        public virtual Account Account4 { get; set; }

        public virtual Account Account5 { get; set; }

        public virtual Account Account6 { get; set; }

        public virtual Account Account7 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeCollectionFeeTypeMap> FeeCollectionFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeCollection> FeeCollections { get; set; }

        public virtual FeeCycle FeeCycle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeMasterClassMap> FeeMasterClassMaps { get; set; }

        public virtual FeeType FeeType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeStructureFeeMap> FeeStructureFeeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinalSettlementFeeTypeMap> FinalSettlementFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentGroupFeeTypeMap> StudentGroupFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentFeeConcession> StudentFeeConcessions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefundFeeTypeMap> RefundFeeTypeMaps { get; set; }

        public virtual ICollection<CampusTransferFeeTypeMap> CampusTransferFeeTypeMaps { get; set; }
    }
}
