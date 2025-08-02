using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class WRONG_BOOK_FEE_BOOKED_20230620
    {
        public long StudentFeeDueIID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        public long? StudentId { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(50)]
        public string InvoiceNo { get; set; }
        public int? FeeMasterID { get; set; }
        [StringLength(50)]
        public string FeeName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CollectedAmount { get; set; }
        public bool CollectionStatus { get; set; }
        public bool? IsCancelled { get; set; }
    }
}
