namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.FeePeriods")]
    public partial class FeePeriod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeePeriod()
        {
            CreditNoteFeeTypeMaps = new HashSet<CreditNoteFeeTypeMap>();
            FeeCollectionFeeTypeMaps = new HashSet<FeeCollectionFeeTypeMap>();
            FeeDueFeeTypeMaps = new HashSet<FeeDueFeeTypeMap>();
            FeeDueMonthlySplits = new HashSet<FeeDueMonthlySplit>();
            FeeMasterClassMaps = new HashSet<FeeMasterClassMap>();
            FeeMasterClassMontlySplitMaps = new HashSet<FeeMasterClassMontlySplitMap>();
            FeeStructureFeeMaps = new HashSet<FeeStructureFeeMap>();
            FinalSettlementFeeTypeMaps = new HashSet<FinalSettlementFeeTypeMap>();
            RefundFeeTypeMaps = new HashSet<RefundFeeTypeMap>();
            StudentFeeConcessions = new HashSet<StudentFeeConcession>();
            StudentGroupFeeTypeMaps = new HashSet<StudentGroupFeeTypeMap>();
            StudentRouteMonthlySplits = new HashSet<StudentRouteMonthlySplit>();
            StudentRoutePeriodMaps = new HashSet<StudentRoutePeriodMap>();
            StudentRouteStopMaps = new HashSet<StudentRouteStopMap>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeePeriodID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public DateTime PeriodFrom { get; set; }

        public DateTime PeriodTo { get; set; }

        public int? AcademicYearId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? NumberOfPeriods { get; set; }

        public byte? SchoolID { get; set; }

        public byte? FeePeriodTypeID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeCollectionFeeTypeMap> FeeCollectionFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueMonthlySplit> FeeDueMonthlySplits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeMasterClassMap> FeeMasterClassMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeMasterClassMontlySplitMap> FeeMasterClassMontlySplitMaps { get; set; }

        public virtual School School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeStructureFeeMap> FeeStructureFeeMaps { get; set; }

        public virtual FeePeriodType FeePeriodType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinalSettlementFeeTypeMap> FinalSettlementFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefundFeeTypeMap> RefundFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentFeeConcession> StudentFeeConcessions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentGroupFeeTypeMap> StudentGroupFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteMonthlySplit> StudentRouteMonthlySplits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRoutePeriodMap> StudentRoutePeriodMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps { get; set; }
    }
}
