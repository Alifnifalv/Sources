using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class OrderChangeRequestSearchView
    {
        public long HeadIID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [Required]
        public string DetailIID { get; set; }
        [Required]
        public string Quantity { get; set; }
        [Required]
        public string UnitPrice { get; set; }
        [Required]
        public string Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ExchangeRate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ReferenceTransactionNo { get; set; }
    }
}
