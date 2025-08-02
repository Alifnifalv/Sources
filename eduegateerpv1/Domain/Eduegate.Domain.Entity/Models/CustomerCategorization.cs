using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerCategorization
    {
        [Key]
        public int SlabID { get; set; }
        public Nullable<int> SlabPoint { get; set; }
        public string CategoryName { get; set; }
        public Nullable<decimal> ExpressDelivery { get; set; }
        public Nullable<decimal> NextDayDelivery { get; set; }
        public Nullable<bool> Active { get; set; }
        public string CategoryNameAr { get; set; }
    }
}
