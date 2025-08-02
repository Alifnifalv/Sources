using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Acc_Security_Deposit_20221102
    {
        [StringLength(100)]
        [Unicode(false)]
        public string AdmissionNumber { get; set; }
        public long? StudentID { get; set; }
        public int? DocumentTypeID { get; set; }
        public long TH_ID { get; set; }
        public long? Ext_Ref_ID { get; set; }
        public int? AccountTransactionHeadID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string VoucherNo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string Narration { get; set; }
        public int? AccountID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
    }
}
