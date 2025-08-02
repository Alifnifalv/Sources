using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CampusTransferMonthlySplit", Schema = "schools")]
    public partial class CampusTransferMonthlySplit
    {
        [Key]
        public long CampusTransferMonthlySplitIID { get; set; }
        public long CampusTransferFeeTypeMapsID { get; set; }
        public int MonthID { get; set; }
        public long? FeeDueMonthlySplitID { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Recievable { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Payable { get; set; }

        [ForeignKey("CampusTransferFeeTypeMapsID")]
        [InverseProperty("CampusTransferMonthlySplits")]
        public virtual CampusTransferFeeTypeMap CampusTransferFeeTypeMaps { get; set; }
        [ForeignKey("FeeDueMonthlySplitID")]
        [InverseProperty("CampusTransferMonthlySplits")]
        public virtual FeeDueMonthlySplit FeeDueMonthlySplit { get; set; }
    }
}
