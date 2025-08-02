using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderMissionReturnExchangeMaster
    {
        public long OrderMissonReturnExchangeMasterID { get; set; }
        public string MissionReturnExchangeName { get; set; }
        public int RefDriverMasterID { get; set; }
        public string Status { get; set; }
        public int CreatedByID { get; set; }
        public System.DateTime CreatedDateTimeStamp { get; set; }
        public bool IsActive { get; set; }
        public string ClosingRemark { get; set; }
    }
}
