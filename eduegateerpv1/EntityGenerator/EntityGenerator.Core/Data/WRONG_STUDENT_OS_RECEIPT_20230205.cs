using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class WRONG_STUDENT_OS_RECEIPT_20230205
    {
        public long? Ext_Ref_ID { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal? Fee_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount { get; set; }
        public int? CNT { get; set; }
    }
}
