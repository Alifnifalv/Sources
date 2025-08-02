using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class JobOperationView
    {
        public int? CompanyID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public long JobEntryHeadIID { get; set; }
        [StringLength(50)]
        public string JobNumber { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JobStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JobEndDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeliveryDate { get; set; }
        [StringLength(50)]
        public string EntitlementName { get; set; }
        [StringLength(100)]
        public string deliverytype { get; set; }
        public int? ReferenceDocumentTypeID { get; set; }
        [StringLength(102)]
        public string TransactionTypeName { get; set; }
        public long? TransactionHeadID { get; set; }
        public byte? PriorityID { get; set; }
        [StringLength(50)]
        public string PriorityDescription { get; set; }
        public int? JobStatusID { get; set; }
        [StringLength(50)]
        public string JobStatusName { get; set; }
        [StringLength(500)]
        public string TransDescription { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(20)]
        public string BasketCode { get; set; }
        public int? DocumentTypeID { get; set; }
        [Required]
        public string PartNumber { get; set; }
        [Required]
        public string BarCode { get; set; }
        public int? TransDocumentTypeID { get; set; }
        [StringLength(50)]
        public string TransTransactionTypeName { get; set; }
        public long? TransHeadID { get; set; }
        [StringLength(308)]
        public string TransactionNo { get; set; }
        public int? DeliveryTimeSlotID { get; set; }
        [StringLength(63)]
        [Unicode(false)]
        public string slotHours { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransDeliveryDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [StringLength(50)]
        public string CityName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public string RemainingHours { get; set; }
        [StringLength(636)]
        public string AddressDetails { get; set; }
        public int? AreaID { get; set; }
        [StringLength(50)]
        public string Area { get; set; }
        public int? RouteID { get; set; }
        public string Routes { get; set; }
        [StringLength(511)]
        public string CustomerDetails { get; set; }
        [StringLength(50)]
        public string MobileNo1 { get; set; }
        [StringLength(50)]
        public string OrderSize { get; set; }
        public byte? JobOperationStatusID { get; set; }
        [StringLength(50)]
        public string OperationStatus { get; set; }
        [StringLength(50)]
        public string GroupName { get; set; }
        [StringLength(767)]
        public string Customer { get; set; }
        public long? ShoppingCartID { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        [StringLength(255)]
        public string SupplierName { get; set; }
        public int CommentCounts { get; set; }
        public byte TransactionStatusID { get; set; }
        public long DocumentStatusID { get; set; }
        public int ReferenceTypeID { get; set; }
    }
}
