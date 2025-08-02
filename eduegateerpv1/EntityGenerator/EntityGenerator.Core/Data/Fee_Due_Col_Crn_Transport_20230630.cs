using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Fee_Due_Col_Crn_Transport_20230630
    {
        public int? SchoolID { get; set; }
        public bool? IsOpening { get; set; }
        public int FiscalYear_ID { get; set; }
        public int DocumentTypeID { get; set; }
        public int? StudentID { get; set; }
        public long Ext_Ref_ID { get; set; }
        [StringLength(100)]
        public string InvoiceNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string AdmissionNumber { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Fee_Amount { get; set; }
        public int? TH_ID { get; set; }
        public bool? Fee_IsCancelled { get; set; }
        public bool? Acc_IsCancelled { get; set; }
        public bool? IsCompleted { get; set; }
        public int CompanyID { get; set; }
    }
}
