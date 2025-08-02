using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductStockBatchPartNo
    {
        [Key]
        public long BatchNo { get; set; }
        public string ProductPartNo { get; set; }
        public string Quantity { get; set; }
    }
}
