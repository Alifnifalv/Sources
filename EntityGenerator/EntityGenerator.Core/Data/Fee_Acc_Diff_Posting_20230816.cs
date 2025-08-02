using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Fee_Acc_Diff_Posting_20230816
    {
        public long? SL_AccountID { get; set; }
        public bool? IsActive { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal? Fee_OS { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_OS { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal? Fee_Acc_Diff { get; set; }
        public int? CompanyID { get; set; }
    }
}
