using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Acc_Transactions_2023_20231120
    {
        public long? TH_ID { get; set; }
        public int IsOpening { get; set; }
        public int? DocumentTypeID { get; set; }
        public int AccountID { get; set; }
        public long? Ext_Ref_ID { get; set; }
        public long? Ext_Ref_Map_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? TranDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string VoucherNo { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount { get; set; }
        public long StudentID { get; set; }
    }
}
