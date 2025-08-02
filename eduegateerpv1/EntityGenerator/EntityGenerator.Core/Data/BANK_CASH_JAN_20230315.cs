using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class BANK_CASH_JAN_20230315
    {
        public int CB_Grp_ID { get; set; }
        [Required]
        [StringLength(4)]
        [Unicode(false)]
        public string CB_Grp_Caption { get; set; }
        public int CompanyID { get; set; }
        [StringLength(100)]
        public string CompanyCode { get; set; }
        [StringLength(100)]
        public string CompanyName { get; set; }
        public int? FiscalYear_ID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FiscalYear_Name { get; set; }
        public int? Main_Group_ID { get; set; }
        [StringLength(50)]
        public string Main_GroupCode { get; set; }
        [StringLength(100)]
        public string Main_GroupName { get; set; }
        public int? Sub_Group_ID { get; set; }
        [StringLength(50)]
        public string Sub_GroupCode { get; set; }
        [StringLength(100)]
        public string Sub_GroupName { get; set; }
        public int GroupID { get; set; }
        [StringLength(50)]
        public string GroupCode { get; set; }
        [StringLength(100)]
        public string GroupName { get; set; }
        public long AccountID { get; set; }
        [Required]
        [StringLength(30)]
        [Unicode(false)]
        public string AccountCode { get; set; }
        [StringLength(500)]
        public string AccountName { get; set; }
        [Column(TypeName = "money")]
        public decimal? Debit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Credit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        [Required]
        [StringLength(2000)]
        [Unicode(false)]
        public string Narration { get; set; }
        [Required]
        [StringLength(2000)]
        [Unicode(false)]
        public string Narration1 { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Card_Name { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Cheque_No { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Cheque_Date { get; set; }
        public long TH_ID { get; set; }
        public int? TranNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TranDate { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string VoucherNo { get; set; }
        public int SlNo { get; set; }
        public int CreatedBy { get; set; }
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string UserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int? TenderTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string TenderTypeName { get; set; }
        public int Currency_ID { get; set; }
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string Currency_Name { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        public string TransactionTypeName { get; set; }
        [StringLength(50)]
        public string TransactionNoPrefix { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string PartCode { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Particulars { get; set; }
    }
}
