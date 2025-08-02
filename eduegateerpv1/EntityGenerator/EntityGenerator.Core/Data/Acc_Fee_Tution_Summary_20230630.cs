using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Acc_Fee_Tution_Summary_20230630
    {
        public int? CompanyID { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Fee_Amount { get; set; }
    }
}
