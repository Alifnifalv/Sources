using Eduegate.Domain.Entity.Models.Inventory;
using Eduegate.Domain.Entity.Models.Workflows;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("TransactionHead", Schema = "inventory")]
    public partial class TransactionHead
    {
        public TransactionHead()
        {
            this.InvetoryTransactions = new List<InvetoryTransaction>();
            this.ShoppingCarts = new List<ShoppingCart>();
            this.JobEntryHeads = new List<JobEntryHead>();
            this.OrderContactMaps = new List<OrderContactMap>();
            this.OrderTrackings = new List<OrderTracking>();
            this.TransactionHead1 = new List<TransactionHead>();
            this.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMap>();
            this.TransactionHeadPointsMaps = new List<TransactionHeadPointsMap>();
            this.TransactionHeadShoppingCartMaps = new List<TransactionHeadShoppingCartMap>();
            this.TransactionShipments = new List<TransactionShipment>();
            this.TransactionDetails = new List<TransactionDetail>();
            this.TransactionHeadAccounts = new List<TransactionHeadAccountMap>();
            this.OrderRoleTrackings = new List<OrderRoleTracking>();
            this.TransactionHeadPayablesMaps = new List<TransactionHeadPayablesMap>();
            this.TransactionHeadReceivablesMaps = new List<TransactionHeadReceivablesMap>();
            this.OrderDeliveryDisplayHeadMaps = new List<OrderDeliveryDisplayHeadMap>();
            this.TaxTransactions = new List<TaxTransaction>();
            this.WorkflowTransactionHeadMaps = new List<WorkflowTransactionHeadMap>();
            this.TransactionHeadAccountMaps = new HashSet<TransactionHeadAccountMap>();
            FeeDueInventoryMaps = new HashSet<FeeDueInventoryMap>();
            RFQSupplierRequestMapHeads = new HashSet<RFQSupplierRequestMap>();
            RFQSupplierRequestMapPurchaseRequests = new HashSet<RFQSupplierRequestMap>();
        }

        [Key]
        public long HeadIID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<long> BranchID { get; set; }
        public Nullable<long> ToBranchID { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public string TransactionNo { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<byte> TransactionStatusID { get; set; }
        public Nullable<byte> EntitlementID { get; set; }
        public Nullable<long> ReferenceHeadID { get; set; }
        public Nullable<long> JobEntryHeadID { get; set; }
        public Nullable<short> DeliveryMethodID { get; set; }
        public Nullable<int> DeliveryDays { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<bool> IsShipment { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public Nullable<decimal> DeliveryCharge { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<int> JobStatusID { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<int> TransactionRole { get; set; }
        public Nullable<int> ReceivingMethodID { get; set; }
        public Nullable<int> ReturnMethodID { get; set; }
        public string ExternalReference1 { get; set; }
        public string ExternalReference2 { get; set; }

        public long? StudentID { get; set; }

        public long? StaffID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        //public virtual Student Student { get; set; }

        //public virtual AcademicYear AcademicYear { get; set; }

        public virtual Schools School { get; set; }
        public decimal? ForeignAmount { get; set; }
        public decimal? TotalLandingCost { get; set; }
        public decimal? LocalDiscount { get; set; }
        public decimal? InvoiceLocalAmount { get; set; }
        public decimal? InvoiceForeignAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public int? DeliveryTimeslotID { get; set; }

        public long? DepartmentID { get; set; }
        public long? ApprovedBy { get; set; }
        public long? BidID { get; set; }
        public string Remarks { get; set; }

        public int? FiscalYear_ID { get; set; }

        public virtual Role Role { get; set; }
        public virtual DeliveryType DeliveryType { get; set; }
        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
        public virtual ICollection<OrderContactMap> OrderContactMaps { get; set; }
        public virtual ICollection<OrderTracking> OrderTrackings { get; set; }
        public virtual ICollection<TransactionHeadAccountMap> TransactionHeadAccounts { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Branch Branch1 { get; set; }
        public virtual Company Company { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual DocumentReferenceStatusMap DocumentReferenceStatusMap { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }
        public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }
        public virtual JobEntryHead JobEntryHead { get; set; }
        public virtual JobStatus JobStatus { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ReceivingMethod ReceivingMethod { get; set; }
        public virtual ReturnMethod ReturnMethod { get; set; }
        public virtual ICollection<TransactionHead> TransactionHead1 { get; set; }
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
        public virtual TransactionHead TransactionHead2 { get; set; }
        public virtual TransactionStatus TransactionStatus { get; set; }
        public virtual ICollection<TransactionHeadEntitlementMap> TransactionHeadEntitlementMaps { get; set; }
        public virtual ICollection<TransactionHeadPointsMap> TransactionHeadPointsMaps { get; set; }
        public virtual ICollection<TransactionHeadShoppingCartMap> TransactionHeadShoppingCartMaps { get; set; }
        public virtual ICollection<TransactionShipment> TransactionShipments { get; set; }

        public virtual ICollection<OrderRoleTracking> OrderRoleTrackings { get; set; }

        public virtual ICollection<TransactionHeadPayablesMap> TransactionHeadPayablesMaps { get; set; }
        public virtual ICollection<TransactionHeadReceivablesMap> TransactionHeadReceivablesMaps { get; set; }
        public virtual ICollection<OrderDeliveryDisplayHeadMap> OrderDeliveryDisplayHeadMaps { get; set; }
        public Nullable<System.DateTime> DocumentCancelledDate { get; set; }
        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
        public virtual ICollection<WorkflowTransactionHeadMap> WorkflowTransactionHeadMaps { get; set; }

        public virtual ICollection<AccountTransactionInventoryHeadMap> AccountTransactionInventoryHeadMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHeadAccountMap> TransactionHeadAccountMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueInventoryMap> FeeDueInventoryMaps { get; set; }

        public virtual Employee ApprovedByNavigation { get; set; }
        public virtual Department1 Department { get; set; }

        public virtual ICollection<RFQSupplierRequestMap> RFQSupplierRequestMapHeads { get; set; }

        public virtual ICollection<RFQSupplierRequestMap> RFQSupplierRequestMapPurchaseRequests { get; set; }

        public virtual BidApprovalHead Bid { get; set; }

    }
}