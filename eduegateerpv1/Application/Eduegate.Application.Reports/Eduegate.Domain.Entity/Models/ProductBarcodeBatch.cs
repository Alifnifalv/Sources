using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductBarcodeBatch
    {
        public long BatchNo { get; set; }
        public int ProductID { get; set; }
        public string ProductBarCode { get; set; }
        public Nullable<bool> IsDuplicateOrNot { get; set; }
    }
}
