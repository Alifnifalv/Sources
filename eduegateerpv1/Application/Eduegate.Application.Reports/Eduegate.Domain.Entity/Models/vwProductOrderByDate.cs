using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductOrderByDate
    {
        public int RefOrderProductID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int OrderQuantity { get; set; }
        public int RefOrderID { get; set; }
    }
}
