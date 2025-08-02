using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ProductSKUVariant
    {
        public long ProductSKUMapIID { get; set; }
        [StringLength(50)]
        public string PropertyTypeName { get; set; }
        [StringLength(50)]
        public string PropertyName { get; set; }
        public long ProductIID { get; set; }
        public int CultureID { get; set; }
        [Required]
        [StringLength(1000)]
        public string Expression { get; set; }
    }
}
