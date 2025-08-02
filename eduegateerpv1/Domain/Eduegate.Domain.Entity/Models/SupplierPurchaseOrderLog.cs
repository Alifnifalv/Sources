using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SupplierPurchaseOrderLog
    {
        [Key]
        public int SupplierPurchaseOrderLogID { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
