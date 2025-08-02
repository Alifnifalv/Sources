namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.WishList")]
    public partial class WishList
    {
        [Key]
        public long WishListIID { get; set; }

        public long? CustomerID { get; set; }

        public long? SKUID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool? IsWishList { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
