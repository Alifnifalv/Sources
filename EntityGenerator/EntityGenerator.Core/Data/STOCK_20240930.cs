using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class STOCK_20240930
    {
        public int RW { get; set; }
        public long? AccountID { get; set; }
        public int? CompanyID { get; set; }
        [Column(TypeName = "money")]
        public decimal? OP_StockValue { get; set; }
        [Column(TypeName = "money")]
        public decimal? Cl_StockValue { get; set; }
    }
}
