using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_General_Entries
    {
        public int? AccountID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TranDate { get; set; }
        public int? TranNo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string VoucherNo { get; set; }
        public int? Yr { get; set; }
        public int? Mn { get; set; }
        public int? Dy { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        [StringLength(100)]
        public string Main_Group_Name { get; set; }
        [StringLength(100)]
        public string Sub_Group_Name { get; set; }
        [StringLength(100)]
        public string GroupName { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string Alias { get; set; }
        [StringLength(50)]
        public string AccountName { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Debit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Credit { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Cheque_No { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Cheque_Date { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string Narration { get; set; }
        [Required]
        [StringLength(2000)]
        [Unicode(false)]
        public string Narration1 { get; set; }
    }
}
