using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DIFF_STUDENT_OS_20230616
    {
        public long? StudentIID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Fee_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount { get; set; }
    }
}
