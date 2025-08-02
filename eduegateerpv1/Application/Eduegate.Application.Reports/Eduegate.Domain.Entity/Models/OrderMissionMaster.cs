using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderMissionMaster
    {
        public long OrderMissionMasterID { get; set; }
        public string MissionName { get; set; }
        public int RefDriverMasterID { get; set; }
        public long CreatedByID { get; set; }
        public System.DateTime CreatedDateTimeStamp { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
