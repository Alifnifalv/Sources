using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductStockBatch
    {
        [Key]
        public long BatchNo { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
