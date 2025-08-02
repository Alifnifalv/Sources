using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VoucherSummaryView
    {
        public int? TotalVouchers { get; set; }
        public int? ActiveVouchers { get; set; }
        public int? MarketingVouchers { get; set; }
        public int? Freeouchers { get; set; }
    }
}
