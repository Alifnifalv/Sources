using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class PurchaseInvoiceSearchView
    {
        public long HeadIID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(152)]
        public string Supplier { get; set; }
        public long? LoginID { get; set; }
        public long? SupplierID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public int? DocumentTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeliveryDate { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(100)]
        public string CreatedUserName { get; set; }
        [StringLength(100)]
        public string UpdatedUserName { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public string EntitlementName { get; set; }
        public string ShoppingCartIID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Amount { get; set; }
        public string PurchaseOrder { get; set; }
        [StringLength(500)]
        public string Reference { get; set; }
        public string ProductOwner { get; set; }
        public int? CompanyID { get; set; }
        [Required]
        public string PartNumber { get; set; }
        [StringLength(20)]
        public string DisplayCode { get; set; }
        public int CommentCounts { get; set; }
        [StringLength(100)]
        public string WorkflowStatus { get; set; }
        public string GRNNo { get; set; }
    }
}
