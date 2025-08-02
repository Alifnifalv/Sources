using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Dup_Fee_Receipt_No_20230107
    {
        [StringLength(255)]
        public string FeeReceiptNo { get; set; }
        public double? CNT { get; set; }
        [StringLength(255)]
        public string F3 { get; set; }
        [StringLength(255)]
        public string New_FeeReceiptNo { get; set; }
    }
}
