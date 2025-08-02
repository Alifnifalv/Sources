using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("thumamaModifiedPhyQty")]
    public partial class thumamaModifiedPhyQty
    {
        [StringLength(255)]
        public string ProductCode { get; set; }
        [StringLength(255)]
        public string SKUName { get; set; }
        public double? PhysicalQty { get; set; }
        public int? ProductID { get; set; }
    }
}
