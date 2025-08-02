using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerByUser
    {
        public int CustomerByUserID { get; set; }
        public long RefCustomerID { get; set; }
        public long RefUserID { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
