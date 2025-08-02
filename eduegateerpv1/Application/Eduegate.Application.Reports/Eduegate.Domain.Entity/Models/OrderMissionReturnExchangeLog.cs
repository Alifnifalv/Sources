using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderMissionReturnExchangeLog
    {
        public long OrderMissionReturnExchangeLogID { get; set; }
        public long OrderMissonReturnExchangeMasterID { get; set; }
        public string OrderMissionReturnExchangeStatus { get; set; }
        public int RefProductID { get; set; }
        public long RefOrderID { get; set; }
        public int RefUserID { get; set; }
        public System.DateTime OrderMissionReturnExchangeLogDateTime { get; set; }
        public bool Status { get; set; }
    }
}
