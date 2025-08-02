using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FEE_ADVANCE_CHANGE_TO_FEE_OS_20220825
    {
        public long TH_ID { get; set; }
        public int? AccountID { get; set; }
        public int? CNT { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        public long? MN_TL_ID { get; set; }
    }
}
