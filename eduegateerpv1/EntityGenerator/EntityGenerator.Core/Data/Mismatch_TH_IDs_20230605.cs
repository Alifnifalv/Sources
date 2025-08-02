using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Mismatch_TH_IDs_20230605
    {
        public long TH_ID { get; set; }
        public int? DocumentTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TranDate { get; set; }
        public int? CompanyID { get; set; }
        public int? FiscalYear_ID { get; set; }
        public int? TranNo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        public long? Ext_Ref_ID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
    }
}
