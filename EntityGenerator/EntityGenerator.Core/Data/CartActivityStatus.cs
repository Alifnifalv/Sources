using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CartActivityStatuses", Schema = "inventory")]
    public partial class CartActivityStatus
    {
        public CartActivityStatus()
        {
            ShoppingCartActivityLogs = new HashSet<ShoppingCartActivityLog>();
        }

        [Key]
        public byte CartActivityStatusID { get; set; }
        [StringLength(50)]
        public string ActivtyStatus { get; set; }

        [InverseProperty("CartActivityStatus")]
        public virtual ICollection<ShoppingCartActivityLog> ShoppingCartActivityLogs { get; set; }
    }
}
