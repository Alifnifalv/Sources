using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CAUITION_DEPO_OTHERS_MANUAL_JOURNAL_20230227
    {
        public long TH_ID { get; set; }
        public int? DocumentTypeID { get; set; }
        public int? AccountTransactionHeadID { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string AdmissionNumber { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string Narration { get; set; }
        public long? Ext_Ref_ID { get; set; }
        [StringLength(500)]
        public string AccountName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TranDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string VoucherNo { get; set; }
        public int? CompanyID { get; set; }
        public int? FiscalYear_ID { get; set; }
        public long TL_ID { get; set; }
        public int? AccountID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        public int SlNo { get; set; }
        public int? NewAccountID { get; set; }
        public int? SL_AccountID { get; set; }
    }
}
