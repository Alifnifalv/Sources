using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductStockBatch
    {
        public long BatchNo { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
