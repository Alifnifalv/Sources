using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class PriceSummaryView
    {
        public int? TotalPriceLists { get; set; }
        public int TotalActive { get; set; }
    }
}
