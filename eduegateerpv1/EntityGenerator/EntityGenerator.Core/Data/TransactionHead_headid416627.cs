using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("TransactionHead_headid416627", Schema = "inventory")]
    public partial class TransactionHead_headid416627
    {
        public long HeadIID { get; set; }
        public int? CompanyID { get; set; }
        public long? BranchID { get; set; }
        public long? ToBranchID { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public long? CustomerID { get; set; }
        public long? SupplierID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountAmount { get; set; }
        [Column(TypeName = "decimal(6, 3)")]
        public decimal? DiscountPercentage { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeliveryDate { get; set; }
        public byte? TransactionStatusID { get; set; }
        public byte? EntitlementID { get; set; }
        public long? ReferenceHeadID { get; set; }
        public long? JobEntryHeadID { get; set; }
        public short? DeliveryMethodID { get; set; }
        public int? DeliveryDays { get; set; }
        public int? CurrencyID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ExchangeRate { get; set; }
        public bool? IsShipment { get; set; }
        public long? EmployeeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DeliveryCharge { get; set; }
        public int? DeliveryTypeID { get; set; }
        public int? JobStatusID { get; set; }
        public long? DocumentStatusID { get; set; }
        public int? TransactionRole { get; set; }
        [StringLength(50)]
        public string Reference { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DocumentCancelledDate { get; set; }
        public int? ReceivingMethodID { get; set; }
        public int? ReturnMethodID { get; set; }
        public int? DeliveryTimeslotID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ToalAmount { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ExternalReference1 { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ExternalReference2 { get; set; }
        public long? StudentID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ActualDeliveryCharge { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PaidAmount { get; set; }
        public bool? IsDiscountIncluded { get; set; }
        public long? CanceledBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeliveredDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ForeignAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LocalDiscount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalLandingCost { get; set; }
        public int? FiscalYear_ID { get; set; }
    }
}
