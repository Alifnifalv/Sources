using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ShoppingCartActivityLogCultreDatas", Schema = "inventory")]
    public partial class ShoppingCartActivityLogCultreData
    {
        [Key]
        public long ShoppingCartActivityLogID { get; set; }
        [Key]
        public byte CultreID { get; set; }
        [StringLength(2000)]
        public string Message { get; set; }

        [ForeignKey("CultreID")]
        [InverseProperty("ShoppingCartActivityLogCultreDatas")]
        public virtual Culture Cultre { get; set; }
        [ForeignKey("ShoppingCartActivityLogID")]
        [InverseProperty("ShoppingCartActivityLogCultreDatas")]
        public virtual ShoppingCartActivityLog ShoppingCartActivityLog { get; set; }
    }
}
