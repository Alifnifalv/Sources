using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransactionHead", Schema = "inventory")]
    [Index("CustomerID", Name = "IDX_TransactionHead_CustomerID_CompanyID__BranchID__ToBranchID__DocumentTypeID__TransactionNo__Desc")]
    [Index("DocumentTypeID", "TransactionStatusID", Name = "IDX_TransactionHead_DocumentTypeIDTransactionStatusID_TransactionNo__TransactionDate__StudentID")]
    [Index("DocumentTypeID", Name = "IDX_TransactionHead_DocumentTypeID_BranchID__ReferenceHeadID")]
    [Index("DocumentTypeID", Name = "IDX_TransactionHead_DocumentTypeID_StudentID")]
    [Index("DocumentTypeID", Name = "IDX_TransactionHead_DocumentTypeID_SupplierID__TransactionStatusID")]
    [Index("FiscalYear_ID", "DocumentTypeID", "TransactionDate", Name = "IDX_TransactionHead_FiscalYear_ID_DocumentTypeID_TransactionDate")]
    [Index("FiscalYear_ID", "TransactionDate", Name = "IDX_TransactionHead_FiscalYear_ID_TransactionDate")]
    [Index("TransactionStatusID", Name = "IDX_TransactionHead_TransactionStatusID_DocumentTypeID__TransactionNo__TransactionDate__StudentID")]
    [Index("ExternalReference2", Name = "INDX_THEAD_EXTREF2")]
    [Index("ExternalReference1", Name = "INDX_THEAD_EXT_REF1")]
    [Index("TransactionDate", Name = "THEAD_TRANSDATE")]
    [Index("DocumentTypeID", Name = "idx_TransactionHeadDocumentTypeID")]
    [Index("TransactionStatusID", Name = "idx_TransactionHeadTransactionStatusID")]
    [Index("ReferenceHeadID", Name = "idx_[TransactionHeadReferenceHeadID")]
    public partial class TransactionHead
    {
        public TransactionHead()
        {
            FeeDueInventoryMaps = new HashSet<FeeDueInventoryMap>();
            InverseReferenceHead = new HashSet<TransactionHead>();
            InvetoryTransactions = new HashSet<InvetoryTransaction>();
            JobEntryHeads = new HashSet<JobEntryHead>();
            OrderContactMaps = new HashSet<OrderContactMap>();
            OrderDeliveryDisplayHeadMaps = new HashSet<OrderDeliveryDisplayHeadMap>();
            OrderRoleTrackings = new HashSet<OrderRoleTracking>();
            RFQSupplierRequestMapHeads = new HashSet<RFQSupplierRequestMap>();
            RFQSupplierRequestMapPurchaseRequests = new HashSet<RFQSupplierRequestMap>();
            ShoppingCart1 = new HashSet<ShoppingCart1>();
            TaxTransactions = new HashSet<TaxTransaction>();
            TransactionDetails = new HashSet<TransactionDetail>();
            TransactionHeadAccountMaps = new HashSet<TransactionHeadAccountMap>();
            TransactionHeadChargeMaps = new HashSet<TransactionHeadChargeMap>();
            TransactionHeadEntitlementMaps = new HashSet<TransactionHeadEntitlementMap>();
            TransactionHeadPayablesMaps = new HashSet<TransactionHeadPayablesMap>();
            TransactionHeadPointsMaps = new HashSet<TransactionHeadPointsMap>();
            TransactionHeadReceivablesMaps = new HashSet<TransactionHeadReceivablesMap>();
            TransactionHeadShoppingCartMaps = new HashSet<TransactionHeadShoppingCartMap>();
            TransactionShipments = new HashSet<TransactionShipment>();
            WorkflowTransactionHeadMaps = new HashSet<WorkflowTransactionHeadMap>();
        }

        [Key]
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
        [Column(TypeName = "decimal(18, 6)")]
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
        [StringLength(500)]
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
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? InvoiceLocalAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? InvoiceForeignAmount { get; set; }
        public long? StaffID { get; set; }
        public long? DepartmentID { get; set; }
        public long? ApprovedBy { get; set; }
        public long? BidID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("TransactionHeads")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ApprovedBy")]
        [InverseProperty("TransactionHeadApprovedByNavigations")]
        public virtual Employee ApprovedByNavigation { get; set; }
        [ForeignKey("BidID")]
        [InverseProperty("TransactionHeads")]
        public virtual BidApprovalHead Bid { get; set; }
        [ForeignKey("BranchID")]
        [InverseProperty("TransactionHeadBranches")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("CompanyID")]
        [InverseProperty("TransactionHeads")]
        public virtual Company Company { get; set; }
        [ForeignKey("CurrencyID")]
        [InverseProperty("TransactionHeads")]
        public virtual Currency Currency { get; set; }
        [ForeignKey("CustomerID")]
        [InverseProperty("TransactionHeads")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("DeliveryMethodID")]
        [InverseProperty("TransactionHeads")]
        public virtual DeliveryType DeliveryMethod { get; set; }
        [ForeignKey("DeliveryTypeID")]
        [InverseProperty("TransactionHeads")]
        public virtual DeliveryType1 DeliveryType { get; set; }
        [ForeignKey("DepartmentID")]
        [InverseProperty("TransactionHeads")]
        public virtual Department1 Department { get; set; }
        [ForeignKey("DocumentStatusID")]
        [InverseProperty("TransactionHeads")]
        public virtual DocumentReferenceStatusMap DocumentStatus { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("TransactionHeads")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("TransactionHeadEmployees")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("EntitlementID")]
        [InverseProperty("TransactionHeads")]
        public virtual EntityTypeEntitlement Entitlement { get; set; }
        [ForeignKey("ReceivingMethodID")]
        [InverseProperty("TransactionHeads")]
        public virtual ReceivingMethod ReceivingMethod { get; set; }
        [ForeignKey("ReferenceHeadID")]
        [InverseProperty("InverseReferenceHead")]
        public virtual TransactionHead ReferenceHead { get; set; }
        [ForeignKey("ReturnMethodID")]
        [InverseProperty("TransactionHeads")]
        public virtual ReturnMethod ReturnMethod { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("TransactionHeads")]
        public virtual School School { get; set; }
        [ForeignKey("StaffID")]
        [InverseProperty("TransactionHeadStaffs")]
        public virtual Employee Staff { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("TransactionHeads")]
        public virtual Student Student { get; set; }
        [ForeignKey("SupplierID")]
        [InverseProperty("TransactionHeads")]
        public virtual Supplier Supplier { get; set; }
        [ForeignKey("ToBranchID")]
        [InverseProperty("TransactionHeadToBranches")]
        public virtual Branch ToBranch { get; set; }
        [ForeignKey("TransactionRole")]
        [InverseProperty("TransactionHeads")]
        public virtual Role TransactionRoleNavigation { get; set; }
        [ForeignKey("TransactionStatusID")]
        [InverseProperty("TransactionHeads")]
        public virtual TransactionStatus TransactionStatus { get; set; }
        [InverseProperty("TransactionHead")]
        public virtual ICollection<FeeDueInventoryMap> FeeDueInventoryMaps { get; set; }
        [InverseProperty("ReferenceHead")]
        public virtual ICollection<TransactionHead> InverseReferenceHead { get; set; }
        [InverseProperty("Head")]
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        [InverseProperty("TransactionHead")]
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<OrderContactMap> OrderContactMaps { get; set; }
        [InverseProperty("Head")]
        public virtual ICollection<OrderDeliveryDisplayHeadMap> OrderDeliveryDisplayHeadMaps { get; set; }
        [InverseProperty("TransactionHead")]
        public virtual ICollection<OrderRoleTracking> OrderRoleTrackings { get; set; }
        [InverseProperty("Head")]
        public virtual ICollection<RFQSupplierRequestMap> RFQSupplierRequestMapHeads { get; set; }
        [InverseProperty("PurchaseRequest")]
        public virtual ICollection<RFQSupplierRequestMap> RFQSupplierRequestMapPurchaseRequests { get; set; }
        [InverseProperty("BlockedHead")]
        public virtual ICollection<ShoppingCart1> ShoppingCart1 { get; set; }
        [InverseProperty("Head")]
        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
        [InverseProperty("Head")]
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
        [InverseProperty("TransactionHead")]
        public virtual ICollection<TransactionHeadAccountMap> TransactionHeadAccountMaps { get; set; }
        [InverseProperty("Head")]
        public virtual ICollection<TransactionHeadChargeMap> TransactionHeadChargeMaps { get; set; }
        [InverseProperty("TransactionHead")]
        public virtual ICollection<TransactionHeadEntitlementMap> TransactionHeadEntitlementMaps { get; set; }
        [InverseProperty("Head")]
        public virtual ICollection<TransactionHeadPayablesMap> TransactionHeadPayablesMaps { get; set; }
        [InverseProperty("TransactionHead")]
        public virtual ICollection<TransactionHeadPointsMap> TransactionHeadPointsMaps { get; set; }
        [InverseProperty("Head")]
        public virtual ICollection<TransactionHeadReceivablesMap> TransactionHeadReceivablesMaps { get; set; }
        [InverseProperty("TransactionHead")]
        public virtual ICollection<TransactionHeadShoppingCartMap> TransactionHeadShoppingCartMaps { get; set; }
        [InverseProperty("TransactionHead")]
        public virtual ICollection<TransactionShipment> TransactionShipments { get; set; }
        [InverseProperty("TransactionHead")]
        public virtual ICollection<WorkflowTransactionHeadMap> WorkflowTransactionHeadMaps { get; set; }
    }
}
