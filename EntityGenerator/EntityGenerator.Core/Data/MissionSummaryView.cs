using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class MissionSummaryView
    {
        public int? TotalOrder { get; set; }
        public int? TotalCodOrder { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? TotalCodAmount { get; set; }
    }
}
