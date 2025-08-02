using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class tmpStock31122021
    {
        public double? PRODUCTID { get; set; }
        public double? Thumama { get; set; }
        public double? WestBay { get; set; }
        public double? Meshaf { get; set; }
        public double? TOTAL { get; set; }
        [Column("LandedCostQR ")]
        public double? LandedCostQR_ { get; set; }
        public double? SellingPriceQR { get; set; }
    }
}
