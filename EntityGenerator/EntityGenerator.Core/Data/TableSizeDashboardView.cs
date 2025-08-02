using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TableSizeDashboardView
    {
        [StringLength(128)]
        public string DatabaseName { get; set; }
        public int? SizeMB { get; set; }
    }
}
