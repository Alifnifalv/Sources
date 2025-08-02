using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class stock31122021
    {
        public double? productid { get; set; }
        public int SchoolID { get; set; }
        public double? OpenQty { get; set; }
        public double? LandedCost { get; set; }
        public double? sellPrice { get; set; }
        public long? ProductSKUMapID { get; set; }
    }
}
