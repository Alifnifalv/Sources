using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Credit_Note_Other_Fee_Mismatched_Amount_20230111
    {
        public long StudentFeeDueIID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        public long SchoolCreditNoteIID { get; set; }
        public long StudentIID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        public int FeeMasterID { get; set; }
        [StringLength(50)]
        public string FeeName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(50)]
        public string InvoiceNo { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Due_Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreditNoteDate { get; set; }
        [StringLength(100)]
        public string CreditNoteNumber { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? CreditNote_Amount { get; set; }
    }
}
