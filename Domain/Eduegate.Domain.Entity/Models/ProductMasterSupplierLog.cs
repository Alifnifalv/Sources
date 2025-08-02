using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductMasterSupplierLog
    {
        [Key]
        public int ProductMasterSupplierLogID { get; set; }
        public short RefSupplierID { get; set; }
        public int RefProductMasterID { get; set; }
    }
}
