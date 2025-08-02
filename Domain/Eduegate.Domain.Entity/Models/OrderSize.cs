using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderSize
    {
        [Key]
        public short OrderSizeID { get; set; }
        public string OrderSizeText { get; set; }
        public bool Active { get; set; }
    }
}
