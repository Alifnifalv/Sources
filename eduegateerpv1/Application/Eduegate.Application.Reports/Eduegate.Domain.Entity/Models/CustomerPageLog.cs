using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerPageLog
    {
        public long LogID { get; set; }
        public long CustomerID { get; set; }
        public string PageName { get; set; }
        public System.DateTime LogOn { get; set; }
    }
}
