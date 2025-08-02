using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ShoppingCartWeekDayMaps", Schema = "inventory")]
    public partial class ShoppingCartWeekDayMap
    {
        [Key]
        public long ShoppingCartWeekDayMapIID { get; set; }
        public long? ShoppingCartID { get; set; }
        public byte? WeekDayID { get; set; }

        [ForeignKey("ShoppingCartID")]
        [InverseProperty("ShoppingCartWeekDayMaps")]
        public virtual ShoppingCart1 ShoppingCart { get; set; }
        [ForeignKey("WeekDayID")]
        [InverseProperty("ShoppingCartWeekDayMaps")]
        public virtual Day WeekDay { get; set; }
    }
}
