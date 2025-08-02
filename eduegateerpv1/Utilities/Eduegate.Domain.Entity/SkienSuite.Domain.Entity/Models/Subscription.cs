using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Subscription
    {
        public long SubscriptionIID { get; set; }
        public string SubscriptionEmail { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public string VarificationCode { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
