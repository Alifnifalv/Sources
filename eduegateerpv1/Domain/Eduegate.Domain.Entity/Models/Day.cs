namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Days", Schema = "mutual")]
    public partial class Day
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Day()
        {
            //WeekDays = new HashSet<WeekDay>();
            //ShoppingCarts = new HashSet<ShoppingCart>();
            ShoppingCartWeekDayMaps = new HashSet<ShoppingCartWeekDayMap>();
        }

        [Key]
        public byte DayID { get; set; }

        [StringLength(50)]
        public string DayName { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<WeekDay> WeekDays { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartWeekDayMap> ShoppingCartWeekDayMaps { get; set; }
    }
}
