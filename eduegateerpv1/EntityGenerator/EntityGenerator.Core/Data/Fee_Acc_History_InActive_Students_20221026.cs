using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Fee_Acc_History_InActive_Students_20221026
    {
        public long? TH_ID_Due { get; set; }
        public long? TH_ID_Col { get; set; }
        public long StudentFeeDueIID { get; set; }
        public long? FeeCollectionIID { get; set; }
        public long StudentIID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string PeriodName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Fee_Date_Due { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Fee_Amount_Due { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Fee_Date_Col { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal? Fee_Amount_Col { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Fee_Amount_Bal { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Acc_Date_Due { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount_Due { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Acc_Date_Col { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount_Col { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_Amount_Bal { get; set; }
    }
}
