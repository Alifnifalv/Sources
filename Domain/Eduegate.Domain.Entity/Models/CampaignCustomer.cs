using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CampaignCustomer
    {
        [Key]
        public int RowID { get; set; }
        public Nullable<byte> RefCampaignID { get; set; }
        public Nullable<long> RefCustomerID { get; set; }
        public Nullable<bool> IsCreated { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> RefVoucherID { get; set; }
    }
}
