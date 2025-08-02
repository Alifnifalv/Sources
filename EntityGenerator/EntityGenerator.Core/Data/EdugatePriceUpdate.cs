using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("EdugatePriceUpdate")]
    public partial class EdugatePriceUpdate
    {
        [StringLength(100)]
        public string Itemcode { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? SP { get; set; }
    }
}
