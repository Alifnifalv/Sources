using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ProductCategorySearchSummaryView
    {
        public int? TotalCategories { get; set; }
        public int? ActiveCategories { get; set; }
    }
}
