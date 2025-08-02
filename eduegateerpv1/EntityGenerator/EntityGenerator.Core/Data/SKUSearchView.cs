using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SKUSearchView
    {
        public long ProductSKUMapIID { get; set; }
        public long? ProductID { get; set; }
        [StringLength(1000)]
        public string SKUName { get; set; }
        [StringLength(150)]
        public string ProductSKUCode { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [StringLength(1000)]
        public string ProductName { get; set; }
        [StringLength(50)]
        public string partno { get; set; }
        [StringLength(50)]
        public string barcode { get; set; }
        public string CategoryName { get; set; }
        public string SkuTag { get; set; }
        [StringLength(50)]
        public string BrandName { get; set; }
        public int? CompanyID { get; set; }
    }
}
