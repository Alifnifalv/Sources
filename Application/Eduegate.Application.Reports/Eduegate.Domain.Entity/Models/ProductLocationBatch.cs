using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductLocationBatch
    {
        public long BatchNo { get; set; }
        public int ProductID { get; set; }
        public string Location { get; set; }
    }
}
