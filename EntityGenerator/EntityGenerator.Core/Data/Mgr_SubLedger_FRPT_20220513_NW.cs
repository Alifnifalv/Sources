using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Mgr_SubLedger_FRPT_20220513_NW
    {
        public long Ref_TH_ID { get; set; }
        public int Ref_SlNo { get; set; }
        public long? SlNo { get; set; }
        public int? AccountID { get; set; }
        public int? SL_AccountID { get; set; }
        public int? StudentID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        public int Ref_ID { get; set; }
        public int Correspond_AccountID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string VoucherNo { get; set; }
        public int? DocumentTypeID { get; set; }
    }
}
