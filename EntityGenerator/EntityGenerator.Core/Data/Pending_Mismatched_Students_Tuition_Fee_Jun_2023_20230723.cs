using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Pending_Mismatched_Students_Tuition_Fee_Jun_2023_20230723
    {
        public long? StudentID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Due_Amount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Fee_Due_Amount { get; set; }
    }
}
