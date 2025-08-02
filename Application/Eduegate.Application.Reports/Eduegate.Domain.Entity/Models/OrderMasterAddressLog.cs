using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderMasterAddressLog
    {
        public int OrderMasterAddressLogID { get; set; }
        public long RefOrderID { get; set; }
        public short UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}
