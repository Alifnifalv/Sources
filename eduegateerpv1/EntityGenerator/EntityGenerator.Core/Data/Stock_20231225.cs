using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Stock_20231225
    {
        public long? RowIndex { get; set; }
        public long HeadIID { get; set; }
        public long? BranchID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        public byte? TransactionStatusID { get; set; }
        [Column(TypeName = "money")]
        public decimal? PhysicalQuantity { get; set; }
        [Column(TypeName = "money")]
        public decimal? ActualQuantity { get; set; }
        [Column(TypeName = "money")]
        public decimal? DifferQuantity { get; set; }
        public long? ProductID { get; set; }
        public long? ProductSKUMapID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Trans_Stock { get; set; }
        [Column(TypeName = "money")]
        public decimal? Closing_Stock { get; set; }
    }
}
