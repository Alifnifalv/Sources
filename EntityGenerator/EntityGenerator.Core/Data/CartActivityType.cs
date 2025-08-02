using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CartActivityTypes", Schema = "inventory")]
    public partial class CartActivityType
    {
        public CartActivityType()
        {
            ShoppingCartActivityLogs = new HashSet<ShoppingCartActivityLog>();
        }

        [Key]
        public byte CartActivityTypeID { get; set; }
        [StringLength(50)]
        public string ActivityTypeName { get; set; }

        [InverseProperty("CartActivityType")]
        public virtual ICollection<ShoppingCartActivityLog> ShoppingCartActivityLogs { get; set; }
    }
}
