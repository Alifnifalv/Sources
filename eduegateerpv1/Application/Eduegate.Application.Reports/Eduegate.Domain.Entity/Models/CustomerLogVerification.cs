using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerLogVerification
    {
        public int RowID { get; set; }
        public Nullable<long> RefCustomerID { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<bool> ChangedTo { get; set; }
        public Nullable<System.DateTime> ChangedOn { get; set; }
    }
}
