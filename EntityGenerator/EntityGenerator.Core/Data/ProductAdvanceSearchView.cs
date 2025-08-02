using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ProductAdvanceSearchView
    {
        public long ProductIID { get; set; }
        [StringLength(1000)]
        public string ProductName { get; set; }
        public long ProductSKUMapIID { get; set; }
        public long? ProductID { get; set; }
        [StringLength(1000)]
        public string SKUName { get; set; }
        public long CategoryIID { get; set; }
        [StringLength(100)]
        public string CategoryName { get; set; }
    }
}
