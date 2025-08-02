using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CHANGE_ADVANCE_OUTSTANDING_TRANSPORT_20230203
    {
        public long TH_ID { get; set; }
        public int? AccountID { get; set; }
        public long? MnTL_ID { get; set; }
        public long? MxTL_ID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        public int? CNT { get; set; }
    }
}
