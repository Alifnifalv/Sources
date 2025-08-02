using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FEEDUE_DUP_20221024
    {
        public long? StudentId { get; set; }
        public int? FeeMasterID { get; set; }
        public int? Year { get; set; }
        public int MonthID { get; set; }
        public long StudentFeeDueIID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        public long FeeDueMonthlySplitIID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public bool? IsCancelled { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CollectedAmount { get; set; }
        public bool CollectionStatus { get; set; }
        public long FeeCollMonthlySplitID { get; set; }
        public long? RowIndex { get; set; }
    }
}
