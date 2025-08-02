using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductBarcodeBatch
    {
        [Key]
        public long BatchNo { get; set; }
        public int ProductID { get; set; }
        public string ProductBarCode { get; set; }
        public Nullable<bool> IsDuplicateOrNot { get; set; }
    }
}
