using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AllAccountTransaction
    {
        public long? AccountID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? TotalAmount { get; set; }
    }
}
