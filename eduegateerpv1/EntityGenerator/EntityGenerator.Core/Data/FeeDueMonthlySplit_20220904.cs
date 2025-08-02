using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeDueMonthlySplit_20220904
    {
        public long FeeDueMonthlySplitIID { get; set; }
        public long FeeDueFeeTypeMapsID { get; set; }
        public int MonthID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "numeric(3, 0)")]
        public decimal? TaxPercentage { get; set; }
        [Column(TypeName = "numeric(18, 3)")]
        public decimal? TaxAmount { get; set; }
        public int? FeePeriodID { get; set; }
        public int? Year { get; set; }
        public bool Status { get; set; }
        public long? FeeStructureMontlySplitMapID { get; set; }
        public long? AccountTransactionHeadID { get; set; }
    }
}
