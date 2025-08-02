using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderLogCod
    {
        public int LogID { get; set; }
        public long RefOrderID { get; set; }
        public long RefCustomerID { get; set; }
        public long RefAdminID { get; set; }
        public System.DateTime LogOn { get; set; }
    }
}
