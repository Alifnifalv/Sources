using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FEE_COLL_ACC_
    {
        public long TH_ID { get; set; }
        public long? Ext_Ref_ID { get; set; }
        public int? AccountID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
    }
}
