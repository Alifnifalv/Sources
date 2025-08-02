using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CR_WRONG_ENTRIES_20230318
    {
        public int? DocumentTypeID { get; set; }
        public long? EXT_REF_ID { get; set; }
        public long TH_ID { get; set; }
        public int? TranNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TranDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
    }
}
