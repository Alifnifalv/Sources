
namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("FeePeriods", Schema = "schools")]
    public partial class FeePeriod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeePeriod()
        {
            FeeCollectionFeeTypeMaps = new HashSet<FeeCollectionFeeTypeMap>();
            FeeMasterClassMaps = new HashSet<FeeMasterClassMap>();
            FeeDueMonthlySplits = new HashSet<FeeDueMonthlySplit>();
            FeeMasterClassMontlySplitMaps = new HashSet<FeeMasterClassMontlySplitMap>();
            CreditNoteFeeTypeMaps = new HashSet<CreditNoteFeeTypeMap>();
            StudentRoutePeriodMaps = new HashSet<StudentRoutePeriodMap>();
            StudentRouteMonthlySplits = new HashSet<StudentRouteMonthlySplit>();
            StudentFeeConcessions = new HashSet<StudentFeeConcession>();
            RefundFeeTypeMaps = new HashSet<RefundFeeTypeMap>();
            CampusTransferFeeTypeMaps = new HashSet<CampusTransferFeeTypeMap>();
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

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public int? NumberOfPeriods { get; set; }

        public byte? SchoolID { get; set; }

        public byte? FeePeriodTypeID { get; set; }

        public virtual Schools School { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual FeePeriodType FeePeriodType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeCollectionFeeTypeMap> FeeCollectionFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeMasterClassMap> FeeMasterClassMaps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueMonthlySplit> FeeDueMonthlySplits { get; set; }

     
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeMasterClassMontlySplitMap> FeeMasterClassMontlySplitMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteMonthlySplit> StudentRouteMonthlySplits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRoutePeriodMap> StudentRoutePeriodMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentFeeConcession> StudentFeeConcessions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefundFeeTypeMap> RefundFeeTypeMaps { get; set; }

        public virtual ICollection<CampusTransferFeeTypeMap> CampusTransferFeeTypeMaps { get; set; }
    }
}
