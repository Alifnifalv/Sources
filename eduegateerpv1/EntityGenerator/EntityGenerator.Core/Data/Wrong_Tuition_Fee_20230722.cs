using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Wrong_Tuition_Fee_20230722
    {
        public long? StudentID { get; set; }
        public bool? IsActive { get; set; }
        public long? Account_Sub_ledger_ID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? OS_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Fee_Amount { get; set; }
    }
}
