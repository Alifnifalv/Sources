using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Trantail_SubLedger", Schema = "account")]
    public partial class Trantail_SubLedger
    {
        public int TL_SL_ID { get; set; }
        [Key]
        public long Ref_TH_ID { get; set; }
        [Key]
        public int Ref_SlNo { get; set; }
        [Key]
        public int SlNo { get; set; }
        public int AccountID { get; set; }
        public int SL_AccountID { get; set; }
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public long Ref_ID { get; set; }
        public int Correspond_AccountID { get; set; }
    }
}
