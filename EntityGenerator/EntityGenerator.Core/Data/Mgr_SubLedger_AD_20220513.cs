using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Mgr_SubLedger_AD_20220513
    {
        public long SL_AccountID { get; set; }
        public long StudentIID { get; set; }
        public int AccountID { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal gltr_pstng_no { get; set; }
        [StringLength(2)]
        public string gltr_doc_code { get; set; }
        [StringLength(8)]
        public string gltr_our_doc_no { get; set; }
        [Column(TypeName = "numeric(17, 3)")]
        public decimal? gltr_tran_amt { get; set; }
        [Required]
        [StringLength(4)]
        public string gltr_acct_code { get; set; }
        public int Correspond_AccountID { get; set; }
        public long? Ref_TH_ID { get; set; }
        public long? Ref_SlNo { get; set; }
    }
}
