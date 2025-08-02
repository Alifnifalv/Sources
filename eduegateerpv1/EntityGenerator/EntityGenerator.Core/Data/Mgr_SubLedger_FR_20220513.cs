using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Mgr_SubLedger_FR_20220513
    {
        public long SL_AccountID { get; set; }
        public long StudentIID { get; set; }
        public long AccountID { get; set; }
        public int FeeMasterID { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal sltr_pstng_no { get; set; }
        [Column(TypeName = "date")]
        public DateTime sltr_pstng_date { get; set; }
        [Required]
        [StringLength(2)]
        public string sltr_doc_code { get; set; }
        [Required]
        [StringLength(8)]
        public string sltr_our_doc_no { get; set; }
        [Column(TypeName = "numeric(17, 3)")]
        public decimal? sltr_tran_amt { get; set; }
        public long AccountID_Inc { get; set; }
        public long AccountID_Rec { get; set; }
        public long AccountID_Adv { get; set; }
        public long? Ref_TH_ID { get; set; }
        public long? Ref_SlNo { get; set; }
        public int? Correspond_AccountID { get; set; }
    }
}
