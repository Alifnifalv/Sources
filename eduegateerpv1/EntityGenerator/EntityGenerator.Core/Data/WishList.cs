using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WishList", Schema = "inventory")]
    public partial class WishList
    {
        [Key]
        public long WishListIID { get; set; }
        public long? CustomerID { get; set; }
        public long? SKUID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? IsWishList { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("WishLists")]
        public virtual Customer Customer { get; set; }
    }
}
