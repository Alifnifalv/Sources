using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeePeriods", Schema = "schools")]
    [Index("PeriodFrom", "PeriodTo", Name = "IDX_FeePeriods_From_To")]
    public partial class FeePeriod
    {
        public FeePeriod()
        {
            CampusTransferFeeTypeMaps = new HashSet<CampusTransferFeeTypeMap>();
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

        [Key]
        public int FeePeriodID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PeriodFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PeriodTo { get; set; }
        public int? AcademicYearId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? NumberOfPeriods { get; set; }
        public byte? SchoolID { get; set; }
        public byte? FeePeriodTypeID { get; set; }

        [ForeignKey("AcademicYearId")]
        [InverseProperty("FeePeriods")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("FeePeriodTypeID")]
        [InverseProperty("FeePeriods")]
        public virtual FeePeriodType FeePeriodType { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("FeePeriods")]
        public virtual School School { get; set; }
        [InverseProperty("FeePeriod")]
        public virtual ICollection<CampusTransferFeeTypeMap> CampusTransferFeeTypeMaps { get; set; }
        [InverseProperty("Period")]
        public virtual ICollection<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }
        [InverseProperty("FeePeriod")]
        public virtual ICollection<FeeCollectionFeeTypeMap> FeeCollectionFeeTypeMaps { get; set; }
        [InverseProperty("FeePeriod")]
        public virtual ICollection<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }
        [InverseProperty("FeePeriod")]
        public virtual ICollection<FeeDueMonthlySplit> FeeDueMonthlySplits { get; set; }
        [InverseProperty("FeePeriod")]
        public virtual ICollection<FeeMasterClassMap> FeeMasterClassMaps { get; set; }
        [InverseProperty("FeePeriod")]
        public virtual ICollection<FeeMasterClassMontlySplitMap> FeeMasterClassMontlySplitMaps { get; set; }
        [InverseProperty("FeePeriod")]
        public virtual ICollection<FeeStructureFeeMap> FeeStructureFeeMaps { get; set; }
        [InverseProperty("FeePeriod")]
        public virtual ICollection<FinalSettlementFeeTypeMap> FinalSettlementFeeTypeMaps { get; set; }
        [InverseProperty("FeePeriod")]
        public virtual ICollection<RefundFeeTypeMap> RefundFeeTypeMaps { get; set; }
        [InverseProperty("FeePeriod")]
        public virtual ICollection<StudentFeeConcession> StudentFeeConcessions { get; set; }
        [InverseProperty("FeePeriod")]
        public virtual ICollection<StudentGroupFeeTypeMap> StudentGroupFeeTypeMaps { get; set; }
        [InverseProperty("FeePeriod")]
        public virtual ICollection<StudentRouteMonthlySplit> StudentRouteMonthlySplits { get; set; }
        [InverseProperty("FeePeriod")]
        public virtual ICollection<StudentRoutePeriodMap> StudentRoutePeriodMaps { get; set; }
        [InverseProperty("FeePeriod")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps { get; set; }
    }
}
