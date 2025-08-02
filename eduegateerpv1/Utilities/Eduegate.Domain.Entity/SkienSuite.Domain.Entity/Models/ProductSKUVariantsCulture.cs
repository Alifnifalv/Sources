using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductSKUVariantsCulture
    {
        public long ProductSKUMapIID { get; set; }
        public string PropertyTypeName { get; set; }
        public string PropertyName { get; set; }
        public long ProductIID { get; set; }
    }
}
