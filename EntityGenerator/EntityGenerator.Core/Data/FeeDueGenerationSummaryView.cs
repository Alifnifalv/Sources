using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeDueGenerationSummaryView
    {
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? TotalDueAmount { get; set; }
        public byte? SchoolID { get; set; }
    }
}
