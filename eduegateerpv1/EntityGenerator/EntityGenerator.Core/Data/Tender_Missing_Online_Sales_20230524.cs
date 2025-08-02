using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Tender_Missing_Online_Sales_20230524
    {
        public long StudentIID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string DocumentName { get; set; }
        public long TH_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TranDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        public long? Ext_Ref_ID { get; set; }
        [Required]
        [StringLength(50)]
        public string GroupTransactionNumber { get; set; }
        public string Remarks { get; set; }
        public long FeeCollectionIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CollectionDate { get; set; }
        [StringLength(50)]
        public string FeeReceiptNo { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal? Amount { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long? TransactionHeadID { get; set; }
    }
}
