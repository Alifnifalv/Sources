using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeCollectionFeeTypeMaps", Schema = "schools")]
    [Index("FeeMasterID", Name = "FeeCollectionFeeTypeMaps_FeeMasterID")]
    [Index("FeeCollectionID", Name = "IDX_FeeCollectionFeeTypeMaps_FeeCollectionID")]
    [Index("FeeMasterID", Name = "IDX_FeeCollectionFeeTypeMaps_FeeMasterID")]
    [Index("FeeMasterID", Name = "IDX_FeeCollectionFeeTypeMaps_FeeMasterID_FeeCollectionID__Amount__FeeDueFeeTypeMapsID__CreditNoteAm")]
    [Index("FeeDueFeeTypeMapsID", Name = "IDX_schools_FeeCollectionFeeTypeMaps_FeeDueFeeTypeMapsID")]
    [Index("FeeCollectionID", Name = "_dta_index_FeeCollectionFeeTypeMaps_7_903778377__K2_5_7")]
    [Index("FeeDueFeeTypeMapsID", Name = "idx_FeeCollectionFeeTypeMapsFeeDueFeeTypeMapsID")]
    public partial class FeeCollectionFeeTypeMap
    {
        public FeeCollectionFeeTypeMap()
        {
            FeeCollectionMonthlySplits = new HashSet<FeeCollectionMonthlySplit>();
            FinalSettlementFeeTypeMaps = new HashSet<FinalSettlementFeeTypeMap>();
            RefundFeeTypeMaps = new HashSet<RefundFeeTypeMap>();
        }

        [Key]
        public long FeeCollectionFeeTypeMapsIID { get; set; }
        public long? FeeCollectionID { get; set; }
        public int? FeeMasterID { get; set; }
        public int? FeePeriodID { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
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
        public long? FeeDueFeeTypeMapsID { get; set; }
        public long? FineMasterStudentMapID { get; set; }
        public int? FineMasterID { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? RefundAmount { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? CreditNoteAmount { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Balance { get; set; }
        public long? AccountTransactionHeadID { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? DueAmount { get; set; }

        [ForeignKey("AccountTransactionHeadID")]
        [InverseProperty("FeeCollectionFeeTypeMaps")]
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
        [ForeignKey("FeeCollectionID")]
        [InverseProperty("FeeCollectionFeeTypeMaps")]
        public virtual FeeCollection FeeCollection { get; set; }
        [ForeignKey("FeeDueFeeTypeMapsID")]
        [InverseProperty("FeeCollectionFeeTypeMaps")]
        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMaps { get; set; }
        [ForeignKey("FeeMasterID")]
        [InverseProperty("FeeCollectionFeeTypeMaps")]
        public virtual FeeMaster FeeMaster { get; set; }
        [ForeignKey("FeePeriodID")]
        [InverseProperty("FeeCollectionFeeTypeMaps")]
        public virtual FeePeriod FeePeriod { get; set; }
        [ForeignKey("FineMasterID")]
        [InverseProperty("FeeCollectionFeeTypeMaps")]
        public virtual FineMaster FineMaster { get; set; }
        [ForeignKey("FineMasterStudentMapID")]
        [InverseProperty("FeeCollectionFeeTypeMaps")]
        public virtual FineMasterStudentMap FineMasterStudentMap { get; set; }
        [InverseProperty("FeeCollectionFeeTypeMap")]
        public virtual ICollection<FeeCollectionMonthlySplit> FeeCollectionMonthlySplits { get; set; }
        [InverseProperty("FeeCollectionFeeTypeMaps")]
        public virtual ICollection<FinalSettlementFeeTypeMap> FinalSettlementFeeTypeMaps { get; set; }
        [InverseProperty("FeeCollectionFeeTypeMaps")]
        public virtual ICollection<RefundFeeTypeMap> RefundFeeTypeMaps { get; set; }
    }
}
