using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CampaignCustomer
    {
        public int RowID { get; set; }
        public Nullable<byte> RefCampaignID { get; set; }
        public Nullable<long> RefCustomerID { get; set; }
        public Nullable<bool> IsCreated { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> RefVoucherID { get; set; }
    }
}
