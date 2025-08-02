using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Acc_Due_Col_Crn_Tuition_20230630
    {
        public int? CompanyID { get; set; }
        public bool? IsOpening { get; set; }
        public int? FiscalYear_ID { get; set; }
        public long StudentID { get; set; }
        public int? DocumentTypeID { get; set; }
        public long Ext_Ref_ID { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? TranDate { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Column(TypeName = "money")]
        public decimal? Op_Balance { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount { get; set; }
        public int? TH_ID { get; set; }
        public bool? Fee_IsCancelled { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
