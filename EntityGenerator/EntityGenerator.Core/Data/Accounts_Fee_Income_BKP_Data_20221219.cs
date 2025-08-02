using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Accounts_Fee_Income_BKP_Data_20221219
    {
        public bool? IsDeleted { get; set; }
        public long TH_ID { get; set; }
        public int? AccountTransactionHeadID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        public int? DocumentTypeID { get; set; }
        public int? TranNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TranDate { get; set; }
        public int? CompanyID { get; set; }
        public int? FiscalYear_ID { get; set; }
    }
}
