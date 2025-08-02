using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderReturnExchange
    {
        [Key]
        public long OrderReturnExchangeRequestID { get; set; }
        public long OrderReturnExchangeReceiptID { get; set; }
        public long RefOrderID { get; set; }
        public int RefProductID { get; set; }
        public int Quantity { get; set; }
        public bool ReturnExchange { get; set; }
        public Nullable<bool> IsMission { get; set; }
        public Nullable<int> UpdatedQty { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDateTimeStamp { get; set; }
    }
}
