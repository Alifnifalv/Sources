using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Caution_Deposit_Check_20221108
    {
        [StringLength(255)]
        public string Duplicated { get; set; }
        public double? Opening_2021_As_Per_Excel { get; set; }
        public double? Yr { get; set; }
        [StringLength(255)]
        public string EnrollNumber { get; set; }
        [StringLength(255)]
        public string StudentName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Adm_Date { get; set; }
        public double? Adm_Year { get; set; }
        [StringLength(255)]
        public string Acd_Status { get; set; }
        [StringLength(255)]
        public string ParentCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PostingDate { get; set; }
        public double? PostingNo { get; set; }
        [StringLength(255)]
        public string Doc_Code { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Doc_Date { get; set; }
        public double? Doc_Number { get; set; }
        public double? Acc_Code { get; set; }
        [StringLength(255)]
        public string Acc_Name { get; set; }
        public double? Amount { get; set; }
        public double? Debit { get; set; }
        public double? Credit { get; set; }
        public double? Refund_Amount { get; set; }
        [StringLength(255)]
        public string Narration { get; set; }
        [StringLength(255)]
        public string PaymentMode { get; set; }
        public double? Payment_Ref { get; set; }
        public double? Refund { get; set; }
        [StringLength(255)]
        public string Remark { get; set; }
    }
}
