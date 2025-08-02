using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductStockBatchPartNo
    {
        public long BatchNo { get; set; }
        public string ProductPartNo { get; set; }
        public string Quantity { get; set; }
    }
}
