using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductUsed
    {
        [Key]
        public int RefUsedProductID { get; set; }
        public byte Position { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
    }
}
