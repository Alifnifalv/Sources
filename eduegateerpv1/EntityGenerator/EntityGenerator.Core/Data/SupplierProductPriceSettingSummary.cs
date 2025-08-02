using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SupplierProductPriceSettingSummary
    {
        public int? TotalProducts { get; set; }
        public int? TotalBrands { get; set; }
        public int? TotalBranch { get; set; }
    }
}
