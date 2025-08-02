using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ProductClassDetail_20230505
    {
        [StringLength(255)]
        public string Class { get; set; }
        [StringLength(255)]
        public string ClassData { get; set; }
        [Column("SKU Name")]
        [StringLength(255)]
        public string SKU_Name { get; set; }
        public double? ProductSKUMapIID { get; set; }
        public double? ProductID { get; set; }
        [StringLength(255)]
        public string F6 { get; set; }
        [StringLength(255)]
        public string F7 { get; set; }
        [StringLength(255)]
        public string F8 { get; set; }
        [StringLength(255)]
        public string F9 { get; set; }
    }
}
