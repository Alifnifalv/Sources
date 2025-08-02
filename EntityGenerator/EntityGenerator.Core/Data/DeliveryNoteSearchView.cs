using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DeliveryNoteSearchView
    {
        public long HeadIID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(767)]
        public string Customer { get; set; }
        public long? CustomerIID { get; set; }
        public int? DocumentTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeliveryDate { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [StringLength(50)]
        public string GroupName { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string IsMarketPlace { get; set; }
        public string SalesOrder { get; set; }
        [Required]
        public string PartNumber { get; set; }
        [Required]
        public string BrandName { get; set; }
        [StringLength(50)]
        public string SIStatus { get; set; }
        public string EntitlementName { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? PayableAmount { get; set; }
        public string ShoppingCartIID { get; set; }
        [StringLength(20)]
        public string DisplayCode { get; set; }
        [StringLength(100)]
        public string CusLogin { get; set; }
        [StringLength(100)]
        public string DeliveryTypeName { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public int? CompanyID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? deliverycharge { get; set; }
        public long? CustomerID { get; set; }
        [StringLength(50)]
        public string reference { get; set; }
        [StringLength(71)]
        public string MobileNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int CommentCounts { get; set; }
        [Required]
        [StringLength(12)]
        [Unicode(false)]
        public string ReportName { get; set; }
        [Required]
        [StringLength(19)]
        [Unicode(false)]
        public string ReportTitle { get; set; }
    }
}
