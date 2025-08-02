using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ShoppingCartActivityLogs", Schema = "inventory")]
    public partial class ShoppingCartActivityLog
    {
        public ShoppingCartActivityLog()
        {
            ShoppingCartActivityLogCultreDatas = new HashSet<ShoppingCartActivityLogCultreData>();
        }

        [Key]
        public long ShoppingCartActivityLogIID { get; set; }
        public long? ShoppingCartID { get; set; }
        public long? ShoppingCartItemID { get; set; }
        public byte CartActivityTypeID { get; set; }
        [StringLength(2000)]
        public string Message { get; set; }
        public byte? CartActivityStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiredDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey("CartActivityStatusID")]
        [InverseProperty("ShoppingCartActivityLogs")]
        public virtual CartActivityStatus CartActivityStatus { get; set; }
        [ForeignKey("CartActivityTypeID")]
        [InverseProperty("ShoppingCartActivityLogs")]
        public virtual CartActivityType CartActivityType { get; set; }
        [ForeignKey("ShoppingCartID")]
        [InverseProperty("ShoppingCartActivityLogs")]
        public virtual ShoppingCart1 ShoppingCart { get; set; }
        [ForeignKey("ShoppingCartItemID")]
        [InverseProperty("ShoppingCartActivityLogs")]
        public virtual ShoppingCartItem ShoppingCartItem { get; set; }
        [InverseProperty("ShoppingCartActivityLog")]
        public virtual ICollection<ShoppingCartActivityLogCultreData> ShoppingCartActivityLogCultreDatas { get; set; }
    }
}
