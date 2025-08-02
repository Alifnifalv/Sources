using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductPointsMaster
    {
        public int ProductPointsMasterID { get; set; }
        public int RefProductID { get; set; }
        public short Points { get; set; }
        public short UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
    }
}
