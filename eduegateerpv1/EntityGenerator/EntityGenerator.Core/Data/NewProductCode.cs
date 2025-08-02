using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("NewProductCode")]
    public partial class NewProductCode
    {
        public double? ProductIID { get; set; }
        [StringLength(255)]
        public string ProductSKUCode { get; set; }
        [StringLength(255)]
        public string SKUName { get; set; }
        [StringLength(255)]
        public string PRODUCTCODE { get; set; }
    }
}
