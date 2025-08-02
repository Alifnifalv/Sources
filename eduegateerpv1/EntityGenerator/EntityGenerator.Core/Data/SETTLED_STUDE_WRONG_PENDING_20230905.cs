using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SETTLED_STUDE_WRONG_PENDING_20230905
    {
        public long? SL_AccountID { get; set; }
        public long? StudentId { get; set; }
        public long AccountID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Fee_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Fee_Acc_Diff { get; set; }
        public int CompanyID { get; set; }
    }
}
