using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductStatusBatch
    {
        [Key]
        public long BatchNo { get; set; }
        public int ProductID { get; set; }
    }
}
