using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderOfflineLog
    {
        public long OrderOfflineLogID { get; set; }
        public long RefOrderOfflineID { get; set; }
        public string OrderStatus { get; set; }
        public long UserID { get; set; }
        public System.DateTime LogDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
