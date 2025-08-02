using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SalesOrderSearchView
    {
        public long HeadIID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
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
        public string SINumber { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string IsMarketPlace { get; set; }
        public string ShoppingCartIID { get; set; }
        [StringLength(767)]
        public string Customer { get; set; }
        [StringLength(152)]
        public string Supplier { get; set; }
        [StringLength(71)]
        public string MobileNo { get; set; }
        public int? DocumentTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeliveryDate { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        public string AirWaybillNo { get; set; }
        [Required]
        public string PartNumber { get; set; }
        [Required]
        public string BrandName { get; set; }
        [Required]
        [StringLength(13)]
        [Unicode(false)]
        public string TFrom { get; set; }
        [Required]
        public string SIStatus { get; set; }
        public string EntitlementName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(50)]
        public string GroupName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? deliverycharge { get; set; }
        public long? CustomerID { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }
        [StringLength(150)]
        public string DevicePlatform { get; set; }
        public string JobStatusID { get; set; }
        public string JobStatus { get; set; }
        public string ProductType { get; set; }
        public string CategoryName { get; set; }
        public string ProductOwner { get; set; }
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
        public int CommentCounts { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [Required]
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(555)]
        public string StudentName { get; set; }
        [Required]
        [StringLength(100)]
        public string RouteCode { get; set; }
        public int? DeliveryTimeSlotID { get; set; }
        [Required]
        [StringLength(63)]
        [Unicode(false)]
        public string DeliveryTimeSlot { get; set; }
        public int? ReportName { get; set; }
        [Required]
        [StringLength(17)]
        [Unicode(false)]
        public string ReportTitle { get; set; }
    }
}
