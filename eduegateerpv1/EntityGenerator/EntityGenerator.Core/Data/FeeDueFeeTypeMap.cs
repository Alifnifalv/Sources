using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeDueFeeTypeMaps", Schema = "schools")]
    [Index("FeeMasterID", Name = "IDX_FeeDueFeeTypeMaps_FeeMasterID")]
    [Index("FeeMasterID", Name = "IDX_FeeDueFeeTypeMaps_FeeMasterID_StudentFeeDueID__FeePeriodID__Amount__Status")]
    [Index("FeeDueFeeTypeMapsIID", "StudentFeeDueID", "FeeMasterID", "FeePeriodID", "Status", Name = "IX_FeeDueFeeTypeMaps")]
    [Index("StudentFeeDueID", Name = "idx_FeeDueFeeTypeMapsStudentFeeDueID")]
    [Index("StudentFeeDueID", Name = "schools_FeeDueFeeTypeMaps_StudentFeeDueID")]
    public partial class FeeDueFeeTypeMap
    {
        public FeeDueFeeTypeMap()
        {
            CampusTransferFeeTypeMaps = new HashSet<CampusTransferFeeTypeMap>();
            CreditNoteFeeTypeMaps = new HashSet<CreditNoteFeeTypeMap>();
            FeeCollectionFeeTypeMaps = new HashSet<FeeCollectionFeeTypeMap>();
            FeeDueCancellations = new HashSet<FeeDueCancellation>();
            FeeDueInventoryMaps = new HashSet<FeeDueInventoryMap>();
            FeeDueMonthlySplits = new HashSet<FeeDueMonthlySplit>();
            FinalSettlementFeeTypeMaps = new HashSet<FinalSettlementFeeTypeMap>();
            RefundFeeTypeMaps = new HashSet<RefundFeeTypeMap>();
            StudentFeeConcessions = new HashSet<StudentFeeConcession>();
        }

        [Key]
        public long FeeDueFeeTypeMapsIID { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long? ClassFeeMasterID { get; set; }
        public int? FeePeriodID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "numeric(3, 0)")]
        public decimal? TaxPercentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TaxAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool Status { get; set; }
        public int? FeeMasterID { get; set; }
        public long? FeeMasterClassMapID { get; set; }
        public long? FineMasterStudentMapID { get; set; }
        public int? FineMasterID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CollectedAmount { get; set; }
        public long? FeeStructureFeeMapID { get; set; }
        public long? AccountTransactionHeadID { get; set; }

        [ForeignKey("AccountTransactionHeadID")]
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
        [ForeignKey("ClassFeeMasterID")]
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual ClassFeeMaster ClassFeeMaster { get; set; }
        [ForeignKey("FeeMasterID")]
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual FeeMaster FeeMaster { get; set; }
        [ForeignKey("FeeMasterClassMapID")]
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual FeeMasterClassMap FeeMasterClassMap { get; set; }
        [ForeignKey("FeePeriodID")]
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual FeePeriod FeePeriod { get; set; }
        [ForeignKey("FeeStructureFeeMapID")]
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual FeeStructureFeeMap FeeStructureFeeMap { get; set; }
        [ForeignKey("FineMasterID")]
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual FineMaster FineMaster { get; set; }
        [ForeignKey("FineMasterStudentMapID")]
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual FineMasterStudentMap FineMasterStudentMap { get; set; }
        [ForeignKey("StudentFeeDueID")]
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual StudentFeeDue StudentFeeDue { get; set; }
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual ICollection<CampusTransferFeeTypeMap> CampusTransferFeeTypeMaps { get; set; }
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual ICollection<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual ICollection<FeeCollectionFeeTypeMap> FeeCollectionFeeTypeMaps { get; set; }
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual ICollection<FeeDueCancellation> FeeDueCancellations { get; set; }
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual ICollection<FeeDueInventoryMap> FeeDueInventoryMaps { get; set; }
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual ICollection<FeeDueMonthlySplit> FeeDueMonthlySplits { get; set; }
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual ICollection<FinalSettlementFeeTypeMap> FinalSettlementFeeTypeMaps { get; set; }
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual ICollection<RefundFeeTypeMap> RefundFeeTypeMaps { get; set; }
        [InverseProperty("FeeDueFeeTypeMaps")]
        public virtual ICollection<StudentFeeConcession> StudentFeeConcessions { get; set; }
    }
}
