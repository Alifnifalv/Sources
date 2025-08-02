using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SKUDeliverySetting
    {
        public long ProductSKUMapIID { get; set; }
        [StringLength(150)]
        public string ProductSKUCode { get; set; }
        [StringLength(1000)]
        public string SKUName { get; set; }
        [StringLength(50)]
        public string PartNo { get; set; }
        [StringLength(50)]
        public string BarCode { get; set; }
    }
}
