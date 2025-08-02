using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductDisableQueryBatch
    {
        public long BatchNo { get; set; }
        public int ProductID { get; set; }
    }
}
