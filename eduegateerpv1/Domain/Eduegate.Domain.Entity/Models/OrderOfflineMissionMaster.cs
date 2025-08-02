using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderOfflineMissionMaster
    {
        [Key]
        public long OrderOfflineMissionMasterID { get; set; }
        public string OrderOfflineMissionName { get; set; }
        public Nullable<int> RefDriverMasterID { get; set; }
        public Nullable<long> CreatedByID { get; set; }
        public Nullable<System.DateTime> CreatedDateTimeStamp { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
