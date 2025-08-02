using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Credit_Note_Regular_Diff_20230117
    {
        public double? StudentFeeDueIID { get; set; }
        public double? FeeDueFeeTypeMapsIID { get; set; }
        public double? SchoolCreditNoteIID { get; set; }
        public double? CreditNoteFeeTypeMapIID { get; set; }
        public double? StudentIID { get; set; }
        [StringLength(255)]
        public string AdmissionNumber { get; set; }
        [StringLength(255)]
        public string StudentName { get; set; }
        [StringLength(255)]
        public string FeeName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(255)]
        public string InvoiceNo { get; set; }
        public double? Due_Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreditNoteDate { get; set; }
        [StringLength(255)]
        public string CreditNoteNumber { get; set; }
        public double? CreditNote_Amount { get; set; }
        [StringLength(255)]
        public string FeePeriod { get; set; }
        public double? Diff { get; set; }
    }
}
