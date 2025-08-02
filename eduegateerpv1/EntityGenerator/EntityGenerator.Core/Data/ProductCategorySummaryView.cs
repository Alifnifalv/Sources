using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ProductCategorySummaryView
    {
        public int? TotalCategory { get; set; }
        public int? ActiveCategories { get; set; }
    }
}
