using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ProductSKUMapSKUNamesView
    {
        public long ProductSKUMapIID { get; set; }
        [Required]
        public string SKU { get; set; }
    }
}
