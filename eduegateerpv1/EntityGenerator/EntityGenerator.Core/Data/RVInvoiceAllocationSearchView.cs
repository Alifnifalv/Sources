using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class RVInvoiceAllocationSearchView
    {
        public long ReceivableIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        public long? DocumentStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNumber { get; set; }
        public long? AccountID { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string AccountCode { get; set; }
        [StringLength(81)]
        public string AccountName { get; set; }
        public int? CurrencyID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PaidAmount { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        public string TransactionTypeName { get; set; }
        public byte? TransactionStatusID { get; set; }
        [StringLength(50)]
        public string TransactionStatusesName { get; set; }
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string InvoiceStatus { get; set; }
    }
}
