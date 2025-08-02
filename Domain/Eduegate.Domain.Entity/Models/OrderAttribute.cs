using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderAttribute
    {
        [Key]
        public int OrderAttributeID { get; set; }
        public int RefOrderID { get; set; }
        public short RefOrderSizeID { get; set; }
    }
}
