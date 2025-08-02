using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductDayDealBlocksProduct
    {
        public int RefBlockID { get; set; }
        public long RefProductID { get; set; }
        public byte Position { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<long> RefUserID { get; set; }
    }
}
