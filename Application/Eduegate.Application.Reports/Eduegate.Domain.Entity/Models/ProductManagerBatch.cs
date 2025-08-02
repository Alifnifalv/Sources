using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductManagerBatch
    {
        public long BatchNo { get; set; }
        public long ProductManagerID { get; set; }
        public int ProductID { get; set; }
    }
}
