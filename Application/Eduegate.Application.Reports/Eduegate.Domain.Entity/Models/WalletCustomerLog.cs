using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class WalletCustomerLog
    {
        public long LogId { get; set; }
        public string Guid { get; set; }
        public long CustomerId { get; set; }
        public string CustomerSessionId { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
    }
}
