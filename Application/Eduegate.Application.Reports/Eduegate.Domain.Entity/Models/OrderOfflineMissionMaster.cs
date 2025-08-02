using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderOfflineMissionMaster
    {
        public long OrderOfflineMissionMasterID { get; set; }
        public string OrderOfflineMissionName { get; set; }
        public Nullable<int> RefDriverMasterID { get; set; }
        public Nullable<long> CreatedByID { get; set; }
        public Nullable<System.DateTime> CreatedDateTimeStamp { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
