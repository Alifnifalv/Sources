using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class GiftSlotProduct
    {
        public int RowID { get; set; }
        public Nullable<byte> RefSlotID { get; set; }
        public Nullable<long> RefProductID { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
    }
}
