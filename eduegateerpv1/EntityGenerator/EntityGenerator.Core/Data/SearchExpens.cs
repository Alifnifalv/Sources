using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SearchExpens
    {
        public long AccountTransactionHeadIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNumber { get; set; }
        public int? DocumentTypeID { get; set; }
        public int? PaymentModeID { get; set; }
        public long? AccountID { get; set; }
        public int? CurrencyID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ExchangeRate { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        [StringLength(50)]
        public string AccountName { get; set; }
    }
}
