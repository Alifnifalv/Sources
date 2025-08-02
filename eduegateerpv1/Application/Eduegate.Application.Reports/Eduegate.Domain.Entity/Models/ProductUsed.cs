using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductUsed
    {
        public int RefUsedProductID { get; set; }
        public byte Position { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
    }
}
