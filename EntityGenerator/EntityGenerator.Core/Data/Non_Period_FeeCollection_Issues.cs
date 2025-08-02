using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Non_Period_FeeCollection_Issues
    {
        public long? StudentFeeDueID { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }
        public int? FeeMasterID { get; set; }
        public int? FeePeriodID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? DueAmount { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal? CollAmount { get; set; }
        public int? ReceiptCount { get; set; }
    }
}
