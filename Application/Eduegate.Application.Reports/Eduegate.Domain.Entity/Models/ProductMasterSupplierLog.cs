using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductMasterSupplierLog
    {
        public int ProductMasterSupplierLogID { get; set; }
        public short RefSupplierID { get; set; }
        public int RefProductMasterID { get; set; }
    }
}
