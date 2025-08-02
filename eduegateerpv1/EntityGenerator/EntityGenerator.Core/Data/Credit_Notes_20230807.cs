using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Credit_Notes_20230807
    {
        public bool IsCancelled { get; set; }
        public long SchoolCreditNoteIID { get; set; }
        public long CreditNoteFeeTypeMapIID { get; set; }
        public long FeeDueFeeTypeMapsID { get; set; }
        public int? FeeMasterID { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreditNoteDate { get; set; }
        [StringLength(100)]
        public string CreditNoteNumber { get; set; }
        public int? YearID { get; set; }
        public int? MonthID { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? InvAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MonthlySplit { get; set; }
        [Column(TypeName = "date")]
        public DateTime? MonthlyDate { get; set; }
        public int Ext_Ref_ID { get; set; }
        public int Ext_Ref_Map_ID { get; set; }
        public int TH_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? TranDate { get; set; }
        public bool? IsDeleted { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount { get; set; }
    }
}
