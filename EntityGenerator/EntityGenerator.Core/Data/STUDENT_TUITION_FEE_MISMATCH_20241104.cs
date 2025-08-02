using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class STUDENT_TUITION_FEE_MISMATCH_20241104
    {
        public long? StudentIID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Bal { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Fee_Bal { get; set; }
    }
}
