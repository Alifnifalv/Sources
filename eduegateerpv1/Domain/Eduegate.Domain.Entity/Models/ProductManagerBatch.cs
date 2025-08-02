using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductManagerBatch
    {
        [Key]
        public long BatchNo { get; set; }
        public long ProductManagerID { get; set; }
        public int ProductID { get; set; }
    }
}
