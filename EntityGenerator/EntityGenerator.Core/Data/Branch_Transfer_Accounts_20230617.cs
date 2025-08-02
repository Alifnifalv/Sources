using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Branch_Transfer_Accounts_20230617
    {
        public long? TH_ID { get; set; }
        public int InvTH_ID { get; set; }
        public long? TranNo { get; set; }
        public int? DocumentTypeID { get; set; }
        public int FiscalYear_ID { get; set; }
        public int CompanyID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TranDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string VoucherNo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        public int IsPosted { get; set; }
        public int IsDeleted { get; set; }
        public int Ref_ID { get; set; }
        public int Session_ID { get; set; }
        public int PostedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PostedDate { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate { get; set; }
        public int DeletedBy { get; set; }
        public int? DeletedDate { get; set; }
        public int Station_ID { get; set; }
        public int? AccountTransactionHeadID { get; set; }
        public int CFC_ID { get; set; }
        public int IsReversed { get; set; }
        public int AccountID { get; set; }
        public int InterAccountID { get; set; }
        [Column(TypeName = "decimal(38, 7)")]
        public decimal? Amount { get; set; }
        [StringLength(66)]
        [Unicode(false)]
        public string Narration { get; set; }
        public long? NewTH_ID { get; set; }
    }
}
