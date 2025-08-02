using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class viewMonSale
    {
        [StringLength(30)]
        public string x_val { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? y_val { get; set; }
    }
}
