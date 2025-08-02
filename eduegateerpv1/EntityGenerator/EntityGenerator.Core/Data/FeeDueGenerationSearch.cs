using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeDueGenerationSearch
    {
        [StringLength(50)]
        public string InvoiceNo { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string InvoiceDate { get; set; }
        [StringLength(50)]
        public string Class { get; set; }
        public long StudentIID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcadamicYearID { get; set; }
        [StringLength(502)]
        public string Student { get; set; }
        [StringLength(50)]
        public string Section { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        public long StudentFeeDueIID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? DueAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? BalanceDue { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? CollectedAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal CreditNote { get; set; }
    }
}
