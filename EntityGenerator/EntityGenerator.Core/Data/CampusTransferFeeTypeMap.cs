using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CampusTransferFeeTypeMaps", Schema = "schools")]
    public partial class CampusTransferFeeTypeMap
    {
        public CampusTransferFeeTypeMap()
        {
            CampusTransferMonthlySplits = new HashSet<CampusTransferMonthlySplit>();
        }

        [Key]
        public long CampusTransferFeeTypeMapsIID { get; set; }
        public long? CampusTransferID { get; set; }
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
        public decimal? Recievable { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Payable { get; set; }

        [ForeignKey("CampusTransferID")]
        [InverseProperty("CampusTransferFeeTypeMaps")]
        public virtual CampusTransfer CampusTransfer { get; set; }
        [ForeignKey("FeeDueFeeTypeMapsID")]
        [InverseProperty("CampusTransferFeeTypeMaps")]
        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMaps { get; set; }
        [ForeignKey("FeeMasterID")]
        [InverseProperty("CampusTransferFeeTypeMaps")]
        public virtual FeeMaster FeeMaster { get; set; }
        [ForeignKey("FeePeriodID")]
        [InverseProperty("CampusTransferFeeTypeMaps")]
        public virtual FeePeriod FeePeriod { get; set; }
        [InverseProperty("CampusTransferFeeTypeMaps")]
        public virtual ICollection<CampusTransferMonthlySplit> CampusTransferMonthlySplits { get; set; }
    }
}
