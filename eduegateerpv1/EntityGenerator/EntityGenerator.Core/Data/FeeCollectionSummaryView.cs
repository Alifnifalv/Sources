using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeCollectionSummaryView
    {
        [Column(TypeName = "decimal(38, 4)")]
        public decimal? TotalCollectedAmount { get; set; }
        public byte? SchoolID { get; set; }
    }
}
