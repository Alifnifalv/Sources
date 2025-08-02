using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("WishList", Schema = "inventory")]
    public partial class WishList
    {
        [Key]
        public long WishListIID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<long> SKUID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public bool IsWishList { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
