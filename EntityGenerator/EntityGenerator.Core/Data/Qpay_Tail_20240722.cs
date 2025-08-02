using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Qpay_Tail_20240722
    {
        public long TH_ID { get; set; }
        public long TL_ID { get; set; }
        public int? Old_AccountID { get; set; }
        public long New_AccountID { get; set; }
        public int PaymentModeID { get; set; }
        [StringLength(50)]
        public string PaymentModeName { get; set; }
        [StringLength(500)]
        public string PaymentAccount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReceiptDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ReceiptNo { get; set; }
        [StringLength(500)]
        public string Wrong_AccountName { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
    }
}
