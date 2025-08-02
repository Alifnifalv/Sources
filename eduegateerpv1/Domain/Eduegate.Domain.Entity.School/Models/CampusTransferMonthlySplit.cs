using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("CampusTransferMonthlySplit", Schema = "schools")]
    public partial class CampusTransferMonthlySplit
    {
        [Key]
        public long CampusTransferMonthlySplitIID { get; set; }
        public long CampusTransferFeeTypeMapsID { get; set; }
        public int MonthID { get; set; }
        public long? FeeDueMonthlySplitID { get; set; }
        public decimal? Recievable { get; set; }
        public decimal? Payable { get; set; }
        public virtual CampusTransferFeeTypeMap CampusTransferFeeTypeMaps { get; set; }
        public virtual FeeDueMonthlySplit FeeDueMonthlySplit { get; set; }
    }
}
