namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.ShoppingCartWeekDayMaps")]
    public partial class ShoppingCartWeekDayMap
    {
        [Key]
        public long ShoppingCartWeekDayMapIID { get; set; }

        public long? ShoppingCartID { get; set; }

        public byte? WeekDayID { get; set; }

        public virtual ShoppingCart1 ShoppingCart { get; set; }

        public virtual Day Day { get; set; }
    }
}
