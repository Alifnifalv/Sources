using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentProformaInvoiceReport
    {
        public long StudentFeeDueIID { get; set; }
        public long? StudentId { get; set; }
        public int? FeePeriodID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [StringLength(50)]
        public string InvoiceNo { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcadamicYearID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TaxAmount { get; set; }
        [Column(TypeName = "numeric(3, 0)")]
        public decimal? TaxPercentage { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(502)]
        public string Student { get; set; }
        [StringLength(4000)]
        public string Class { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(63)]
        [Unicode(false)]
        public string Academic { get; set; }
        [StringLength(20)]
        public string NationalIDNo { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [StringLength(500)]
        public string Address1 { get; set; }
        [StringLength(500)]
        public string Address2 { get; set; }
        [StringLength(50)]
        public string SchoolCode { get; set; }
        [StringLength(100)]
        public string Place { get; set; }
    }
}
