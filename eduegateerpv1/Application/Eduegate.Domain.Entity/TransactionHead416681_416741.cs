namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.TransactionHead416681_416741")]
    public partial class TransactionHead416681_416741
    {
        [Key]
        public long HeadIID { get; set; }

        public int? CompanyID { get; set; }

        public long? BranchID { get; set; }

        public long? ToBranchID { get; set; }

        public int? DocumentTypeID { get; set; }

        [StringLength(50)]
        public string TransactionNo { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public long? CustomerID { get; set; }

        public long? SupplierID { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public DateTime? TransactionDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public byte? TransactionStatusID { get; set; }

        public byte? EntitlementID { get; set; }

        public long? ReferenceHeadID { get; set; }

        public long? JobEntryHeadID { get; set; }

        public short? DeliveryMethodID { get; set; }

        public int? DeliveryDays { get; set; }

        public int? CurrencyID { get; set; }

        public decimal? ExchangeRate { get; set; }

        public bool? IsShipment { get; set; }

        public long? EmployeeID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public decimal? DeliveryCharge { get; set; }

        public int? DeliveryTypeID { get; set; }

        public int? JobStatusID { get; set; }

        public long? DocumentStatusID { get; set; }

        public int? TransactionRole { get; set; }

        [StringLength(50)]
        public string Reference { get; set; }

        public DateTime? DocumentCancelledDate { get; set; }

        public int? ReceivingMethodID { get; set; }

        public int? ReturnMethodID { get; set; }

        public int? DeliveryTimeslotID { get; set; }

        public decimal? ToalAmount { get; set; }

        [StringLength(50)]
        public string ExternalReference1 { get; set; }

        [StringLength(50)]
        public string ExternalReference2 { get; set; }

        public long? StudentID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public decimal? ActualDeliveryCharge { get; set; }

        public decimal? TotalAmount { get; set; }

        public decimal? PaidAmount { get; set; }

        public bool? IsDiscountIncluded { get; set; }

        public long? CanceledBy { get; set; }

        public DateTime? DeliveredDate { get; set; }

        public decimal? ForeignAmount { get; set; }

        public decimal? LocalDiscount { get; set; }

        public decimal? TotalLandingCost { get; set; }

        public int? FiscalYear_ID { get; set; }
    }
}
