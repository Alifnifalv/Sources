using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ChartOperationStatu
    {
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string type { get; set; }
        public int? Pending { get; set; }
    }
}
