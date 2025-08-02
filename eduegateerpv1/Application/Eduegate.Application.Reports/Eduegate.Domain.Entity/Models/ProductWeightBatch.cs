using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductWeightBatch
    {
        public long BatchNo { get; set; }
        public int ProductID { get; set; }
        public long ProductWeight { get; set; }
    }
}
