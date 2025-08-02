using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class EdugateProduct
    {
        [StringLength(100)]
        public string ItemCode { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(50)]
        public string Product { get; set; }
        [StringLength(50)]
        public string Size { get; set; }
        [StringLength(50)]
        public string UOM { get; set; }
        public long? Qty { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Cost { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? SP { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Rate { get; set; }
        public long? ProductPriceID { get; set; }
    }
}
