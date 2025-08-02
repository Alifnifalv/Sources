using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    public partial class STS_AuditReceipts_Shabeeb
    {
        [Key]
        public long IID { get; set; }
        public string Rno { get; set; }
        public string ReceivedFrom { get; set; }
        public string AmountInWords { get; set; }
        public string Amount { get; set; }
        public string Year { get; set; }
        public string InSettlementOf { get; set; }
        public string CashOrChequeNo { get; set; }
        public string DrawnOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
    }
}
