using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SupplierProductSKUView
    {
        public long SupplierIID { get; set; }
        public Nullable<long> ProductSKUID { get; set; }
    }
}
