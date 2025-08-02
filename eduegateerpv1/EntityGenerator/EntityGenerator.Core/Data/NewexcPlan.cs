using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("NewexcPlan")]
    public partial class NewexcPlan
    {
        public string text { get; set; }
        [Required]
        [StringLength(20)]
        public string objtype { get; set; }
        [Column(TypeName = "xml")]
        public string query_plan { get; set; }
        public int? RwNum { get; set; }
    }
}
