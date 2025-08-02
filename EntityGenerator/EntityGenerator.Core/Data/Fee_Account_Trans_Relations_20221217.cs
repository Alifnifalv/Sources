using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Fee_Account_Trans_Relations_20221217
    {
        [Column(TypeName = "date")]
        public DateTime? TranDate { get; set; }
        public int? YearID { get; set; }
        public int MonthID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        public int? FeeMasterID { get; set; }
        public long? StudentId { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long FeeDueFeeTypeMapsID { get; set; }
        public int? FeePeriodID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Fee_Due_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Credit_Note_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Fee_Coll_Amount { get; set; }
        public int? Due_TH_ID { get; set; }
        public int? Coll_TH_ID { get; set; }
        public int? Crn_TH_ID { get; set; }
        public int? Inc_TH_ID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Due_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Coll_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Crn_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Inc_Amount { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Acc_Due_TranDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Acc_Coll_TranDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Acc_Crn_TranDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Acc_Inc_TranDate { get; set; }
    }
}
