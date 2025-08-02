using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Pending_Reqular_Fee_2021
    {
        public bool? Is_TC { get; set; }
        public int? Yr { get; set; }
        public long StudentFeeDueIID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(50)]
        public string InvoiceNo { get; set; }
        public int? FeeMasterID { get; set; }
        [StringLength(50)]
        public string FeeName { get; set; }
        public bool? IsActive { get; set; }
        public byte? Status { get; set; }
        public long? StudentId { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Due_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Fee_Col { get; set; }
        [Column(TypeName = "money")]
        public decimal? Fee_Crn { get; set; }
        [Column(TypeName = "money")]
        public decimal? FinalSettlement { get; set; }
    }
}
