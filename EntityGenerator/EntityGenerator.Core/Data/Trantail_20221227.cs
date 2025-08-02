using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Trantail_20221227
    {
        public long TL_ID { get; set; }
        public long TH_ID { get; set; }
        public int? AccountID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? CRate { get; set; }
        [Column(TypeName = "money")]
        public decimal? CRate1 { get; set; }
        [Column(TypeName = "money")]
        public decimal? CRate2 { get; set; }
        [Column(TypeName = "money")]
        public decimal? FAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal? FAmount1 { get; set; }
        [Column(TypeName = "money")]
        public decimal? FAmount2 { get; set; }
        public int? TenderTypeID { get; set; }
        public int? Currency_ID { get; set; }
        public int? Currency_ID1 { get; set; }
        public int? Currency_ID2 { get; set; }
        public int SlNo { get; set; }
        public int? Ref_ID { get; set; }
        public int? Ref_TH_ID { get; set; }
        public int? Ref_SlNo { get; set; }
        public bool? IsPDC { get; set; }
        public int? Ref_Reconcile_ID { get; set; }
        public int? Ref_Reconcile_RowNo { get; set; }
        public bool? Is_SubLedger_Allocated { get; set; }
        public bool? Is_CostCenter_Allocated { get; set; }
        public bool? Is_Budget_Allocated { get; set; }
        public bool? Is_Project_Allocated { get; set; }
        public int? Correspond_SlNo { get; set; }
        public int? Correspond_Acc_ID { get; set; }
    }
}
