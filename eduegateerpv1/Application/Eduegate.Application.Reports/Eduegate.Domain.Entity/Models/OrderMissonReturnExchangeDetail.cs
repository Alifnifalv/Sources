using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderMissonReturnExchangeDetail
    {
        public long OrderMissonReturnExchangeDetailsID { get; set; }
        public long RefOrderMissonReturnExchangeMasterID { get; set; }
        public long RefOrderID { get; set; }
        public long RefOrderReturnExchangeReceiptID { get; set; }
        public int CreatedByID { get; set; }
        public System.DateTime CreatedDateTimeStamp { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> UpdatedByTimeStamp { get; set; }
        public Nullable<int> UpdatedByID { get; set; }
    }
}
