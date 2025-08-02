using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FinalSettlement_Accounts_New_20221021
    {
        public bool? IsDeleted { get; set; }
        public long FinalSettlementIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(50)]
        public string FeeReceiptNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TranDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FinalSettlementDate { get; set; }
        [StringLength(500)]
        public string AccountName { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? AmountPaid { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Final_Amount { get; set; }
        public int? AccountTransactionHeadID { get; set; }
        public long TH_ID { get; set; }
        public int? DocumentTypeID { get; set; }
        public int? TranNo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNumber { get; set; }
    }
}
