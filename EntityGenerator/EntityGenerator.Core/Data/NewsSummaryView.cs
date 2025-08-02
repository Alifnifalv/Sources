using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class NewsSummaryView
    {
        public int? TotalRecords { get; set; }
        public int? OursCount { get; set; }
        public int? MediaCount { get; set; }
        public int? FeaturedCount { get; set; }
    }
}
