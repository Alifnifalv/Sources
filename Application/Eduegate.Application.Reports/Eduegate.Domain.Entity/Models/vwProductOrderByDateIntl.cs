using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductOrderByDateIntl
    {
        public long ProductOrderByDateIntlID { get; set; }
        public short RefCountryID { get; set; }
        public int RefOrderProductID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int OrderQuantity { get; set; }
        public int RefOrderID { get; set; }
    }
}
