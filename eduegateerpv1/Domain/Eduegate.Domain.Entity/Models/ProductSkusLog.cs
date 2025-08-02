using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductSkusLog
    {
        [Key]
        public long ProductSkusLogID { get; set; }
        public int TotalActiveSkus { get; set; }
        public int TotalSkusQuantity { get; set; }
        public int InStockSkus { get; set; }
        public int OssSkus { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
