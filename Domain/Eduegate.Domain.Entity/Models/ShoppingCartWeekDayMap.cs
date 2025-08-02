using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Eduegate.Domain.Entity.Models
{
    [Table("ShoppingCartWeekDayMaps", Schema = "inventory")]
    public partial class ShoppingCartWeekDayMap
    {
        [Key]
        public long ShoppingCartWeekDayMapIID { get; set; }

        public long? ShoppingCartID { get; set; }

        public byte? WeekDayID { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        public virtual Day Day { get; set; }
    }
}
