using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class WishList
    {
        public long WishListIID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<long> SKUID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsWishList { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
