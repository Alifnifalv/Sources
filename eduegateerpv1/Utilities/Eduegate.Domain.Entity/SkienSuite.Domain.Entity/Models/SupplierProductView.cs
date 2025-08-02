using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SupplierProductView
    {
        public long SupplierIID { get; set; }
        public Nullable<long> ProductID { get; set; }
    }
}
