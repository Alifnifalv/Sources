using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.School.Models
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
        public decimal? Amount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }
        public decimal? Recievable { get; set; }
        public decimal? Payable { get; set; }
        public virtual CampusTransfers CampusTransfer { get; set; }
        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMaps { get; set; }
        public virtual FeeMaster FeeMaster { get; set; }
        public virtual FeePeriod FeePeriod { get; set; }
        public virtual ICollection<CampusTransferMonthlySplit> CampusTransferMonthlySplits { get; set; }
    }
}