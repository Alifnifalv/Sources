using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Credit_Notes_Date_Changed_20230808
    {
        public int Ext_Ref_ID { get; set; }
        public int? FeeMasterID { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? InvAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount { get; set; }
    }
}
