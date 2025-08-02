using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderSize
    {
        public short OrderSizeID { get; set; }
        public string OrderSizeText { get; set; }
        public bool Active { get; set; }
    }
}
