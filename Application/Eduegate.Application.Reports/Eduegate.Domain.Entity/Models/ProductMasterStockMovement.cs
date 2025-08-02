using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductMasterStockMovement
    {
        public long ProductMasterStockMovementID { get; set; }
        public short RefCountryID { get; set; }
        public int RefProductID { get; set; }
        public string TransType { get; set; }
        public short TransQuantity { get; set; }
        public short Quantity { get; set; }
        public short QuantityUpdated { get; set; }
        public string RefType { get; set; }
        public string RefValue { get; set; }
        public short UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}
