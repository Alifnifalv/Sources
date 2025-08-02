using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SKUDeliverySetting
    {
        public long ProductSKUMapIID { get; set; }
        public string ProductSKUCode { get; set; }
        public string SKUName { get; set; }
        public string PartNo { get; set; }
        public string BarCode { get; set; }
    }
}
