using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Student_Tuition_Fee_Due_20230722
    {
        public bool CollectionStatus { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CollectedAmount { get; set; }
        public long StudentFeeDueIID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(50)]
        public string InvoiceNo { get; set; }
        public int? FeeMasterID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Due_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Col_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Crn_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Fnl_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Rfn_Amount { get; set; }
    }
}
