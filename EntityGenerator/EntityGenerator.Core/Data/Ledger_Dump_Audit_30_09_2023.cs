using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Ledger_Dump_Audit_30_09_2023
    {
        [StringLength(100)]
        public string CompanyCode { get; set; }
        [StringLength(100)]
        public string CompanyName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? TranDate { get; set; }
        [StringLength(50)]
        public string DocumentName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TranNo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        [StringLength(50)]
        public string Main_GroupCode { get; set; }
        [StringLength(100)]
        public string Main_GroupName { get; set; }
        [StringLength(50)]
        public string Sub_GroupCode { get; set; }
        [StringLength(100)]
        public string Sub_GroupName { get; set; }
        [StringLength(50)]
        public string GroupCode { get; set; }
        [StringLength(100)]
        public string GroupName { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string AccountCode { get; set; }
        [StringLength(500)]
        public string AccountName { get; set; }
        [Required]
        [StringLength(2000)]
        [Unicode(false)]
        public string Narration { get; set; }
        [Column(TypeName = "money")]
        public decimal? Op_Debit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Op_Credit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Tr_Debit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Tr_Credit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Cl_Debit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Cl_Credit { get; set; }
    }
}
