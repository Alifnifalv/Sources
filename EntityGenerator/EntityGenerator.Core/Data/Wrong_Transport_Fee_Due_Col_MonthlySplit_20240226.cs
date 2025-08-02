using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Wrong_Transport_Fee_Due_Col_MonthlySplit_20240226
    {
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }
        public long FeeCollectionFeeTypeMapsIID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [StringLength(50)]
        public string ClassName { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        public bool? IsActive { get; set; }
        public byte? Status { get; set; }
        [StringLength(50)]
        public string FeeReceiptNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CollectionDate { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal? Col_Amount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Pay_Amount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Diff { get; set; }
        [StringLength(50)]
        public string FeeName { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Amount { get; set; }
        public long FeeCollectionIID { get; set; }
        public long? StudentID { get; set; }
        public long StudentFeeDueIID { get; set; }
        public long FeeDueMonthlySplitIID { get; set; }
        public long FeeCollectionMonthlySplitIID { get; set; }
    }
}
