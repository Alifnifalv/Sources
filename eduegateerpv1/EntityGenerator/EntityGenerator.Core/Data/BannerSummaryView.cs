using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class BannerSummaryView
    {
        public int? TotalBanners { get; set; }
        public int? ActiveBanners { get; set; }
    }
}
