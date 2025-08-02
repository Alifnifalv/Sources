using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("westBayModifiedPhyQty")]
    public partial class westBayModifiedPhyQty
    {
        [StringLength(300)]
        public string ProductCode { get; set; }
        [StringLength(2000)]
        public string ProductName { get; set; }
        public int? PhysicalQuantity { get; set; }
        public int? NewPhyQty { get; set; }
    }
}
