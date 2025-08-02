using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CartChargeCultureDatas", Schema = "inventory")]
    public partial class CartChargeCultureData
    {
        [Key]
        public int CartChargeID { get; set; }
        [Key]
        public byte CultureID { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
    }
}
