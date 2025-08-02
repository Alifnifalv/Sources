using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeOutstadingCaseSearchView
    {
        [StringLength(50)]
        public string InvoiceNo { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string InvoiceDate { get; set; }
        public int? ClassID { get; set; }
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
        [Column(TypeName = "decimal(18, 3)")]
        public decimal DueAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? BalanceDue { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal CollectedAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal CreditNote { get; set; }
        public long ParentID { get; set; }
        [StringLength(10)]
        public string ParentCode { get; set; }
        public long? ParentLoginID { get; set; }
        [StringLength(500)]
        public string ParentEmail { get; set; }
        [StringLength(302)]
        public string FatherName { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string DateToday { get; set; }
        public int ReferenceTypeID { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string TicketCreation { get; set; }
        [Required]
        [StringLength(4)]
        [Unicode(false)]
        public string ViewFeeDueStatement { get; set; }
        [Required]
        [StringLength(4)]
        [Unicode(false)]
        public string SendFeeDueStatement { get; set; }
        [Required]
        [StringLength(4)]
        [Unicode(false)]
        public string ViewProformaInvoice { get; set; }
        [Required]
        [StringLength(4)]
        [Unicode(false)]
        public string SendProformaInvoice { get; set; }
    }
}
