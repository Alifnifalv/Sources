using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Transport_Acc_Rec
    {
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        public string DocumentName { get; set; }
        public long? Ext_Ref_ID { get; set; }
        public long? Ext_Ref_Map_ID { get; set; }
        public int? AccountID { get; set; }
        public int? FiscalYear_ID { get; set; }
        public int? CompanyID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TranDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string Narration { get; set; }
        public long? StudentID { get; set; }
        public bool? IsActive { get; set; }
    }
}
