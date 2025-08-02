using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Days", Schema = "mutual")]
    public partial class Day
    {
        public Day()
        {
            ShoppingCartWeekDayMaps = new HashSet<ShoppingCartWeekDayMap>();
            WeekDays = new HashSet<WeekDay>();
        }

        [Key]
        public byte DayID { get; set; }
        [StringLength(50)]
        public string DayName { get; set; }

        [InverseProperty("WeekDay")]
        public virtual ICollection<ShoppingCartWeekDayMap> ShoppingCartWeekDayMaps { get; set; }
        [InverseProperty("Day")]
        public virtual ICollection<WeekDay> WeekDays { get; set; }
    }
}
