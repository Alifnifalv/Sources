using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class OpeningAccount_20220817
    {
        public int OP_Acc_ID { get; set; }
        public int AccountID { get; set; }
        public int CompanyID { get; set; }
        public int FiscalYear_ID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? OP_Date { get; set; }
        [Column(TypeName = "money")]
        public decimal OP_Balance { get; set; }
        [Column(TypeName = "money")]
        public decimal OP_Settled { get; set; }
        [Column(TypeName = "money")]
        public decimal CL_Balance { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        public int? DocumentTypeID { get; set; }
        public int? OP_SlNo { get; set; }
    }
}
