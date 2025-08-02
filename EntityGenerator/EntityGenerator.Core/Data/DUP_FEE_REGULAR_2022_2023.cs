using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DUP_FEE_REGULAR_2022_2023
    {
        public long StudentFeeDueIID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        public long FeeDueMonthlySplitIID { get; set; }
        public long? StudentId { get; set; }
        public int? FeeMasterID { get; set; }
        public int? Year { get; set; }
        public int MonthID { get; set; }
        public long? RowIndex { get; set; }
    }
}
