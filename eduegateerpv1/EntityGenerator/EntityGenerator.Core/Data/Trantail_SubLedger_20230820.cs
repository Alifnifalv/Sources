using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Trantail_SubLedger_20230820
    {
        public int TL_SL_ID { get; set; }
        public long Ref_TH_ID { get; set; }
        public int Ref_SlNo { get; set; }
        public int SlNo { get; set; }
        public int AccountID { get; set; }
        public int SL_AccountID { get; set; }
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public long Ref_ID { get; set; }
        public int Correspond_AccountID { get; set; }
    }
}
