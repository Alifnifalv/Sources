using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderAttribute
    {
        public int OrderAttributeID { get; set; }
        public int RefOrderID { get; set; }
        public short RefOrderSizeID { get; set; }
    }
}
