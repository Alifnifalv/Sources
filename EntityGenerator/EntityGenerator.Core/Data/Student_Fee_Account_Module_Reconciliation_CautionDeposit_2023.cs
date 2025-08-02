using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Student_Fee_Account_Module_Reconciliation_CautionDeposit_2023
    {
        public int? FeeMasterID { get; set; }
        public int? AccountID { get; set; }
        public long? StudentID { get; set; }
        public bool? IsOpening { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsTC { get; set; }
        public bool? IsSettled { get; set; }
        public bool? IsCompleted { get; set; }
        public int? DocumentTypeID { get; set; }
        public long? Ext_Ref_ID { get; set; }
        public long? Ext_Ref_Map_ID { get; set; }
        public long? Ext_Ref_Split_ID { get; set; }
        public long? Ext_Ref_Month_ID { get; set; }
        public long? Ext_Ref_Year_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Acc_TranDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Fee_TranDate { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Fee_Amount { get; set; }
        public long? RExt_Ref_ID { get; set; }
        public long? RExt_Ref_Map_ID { get; set; }
        public long? RExt_Ref_Split_ID { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        public bool? Fee_IsCancelled { get; set; }
        public bool? Acc_IsCancelled { get; set; }
    }
}
