using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Presentation_2021_Audit
    {
        [Column("Row Labels")]
        [StringLength(255)]
        public string Row_Labels { get; set; }
        [Column("Sum of 31-12-21")]
        public double? Sum_of_31_12_21 { get; set; }
        [Column("Sum of 31-12-20")]
        public double? Sum_of_31_12_20 { get; set; }
    }
}
