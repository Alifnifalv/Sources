using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MiniMarketLog
    {
        public int RowID { get; set; }
        public string ActionType { get; set; }
        public string Action { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<System.DateTime> ActionTime { get; set; }
    }
}
