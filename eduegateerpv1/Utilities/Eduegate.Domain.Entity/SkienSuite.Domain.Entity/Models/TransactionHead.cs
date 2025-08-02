using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TransactionHead
    {
        public TransactionHead()
        {
            this.OrderDeliveryDisplayHeadMaps = new List<OrderDeliveryDisplayHeadMap>();
            this.InvetoryTransactions = new List<InvetoryTransaction>();
            this.ShoppingCarts = new List<ShoppingCart1>();
            this.JobEntryHeads = new List<JobEntryHead>();
            this.OrderContactMaps = new List<OrderContactMap>();
            this.TransactionHead1 = new List<TransactionHead>();
            this.TransactionHeadAccountMaps = new List<TransactionHeadAccountMap>();
            this.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMap>();
            this.TransactionHeadPayablesMaps = new List<TransactionHeadPayablesMap>();
            this.TransactionHeadPointsMaps = new List<TransactionHeadPointsMap>();
            this.TransactionHeadReceivablesMaps = new List<TransactionHeadReceivablesMap>();
            this.TransactionHeadShoppingCartMaps = new List<TransactionHeadShoppingCartMap>();
            this.TransactionShipments = new List<TransactionShipment>();
        }

        public long HeadIID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<long> BranchID { get; set; }
        public Nullable<long> ToBranchID { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public string TransactionNo { get; set; }
        public string Description { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<byte> TransactionStatusID { get; set; }
        public Nullable<short> EntitlementID { get; set; }
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
        public byte[] TimeStamps { get; set; }
        public Nullable<decimal> DeliveryCharge { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<int> JobStatusID { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<int> TransactionRole { get; set; }
        public string Reference { get; set; }
        public Nullable<System.DateTime> DocumentCancelledDate { get; set; }
        public Nullable<int> ReceivingMethodID { get; set; }
        public Nullable<int> ReturnMethodID { get; set; }
        public virtual Role Role { get; set; }
        public virtual DeliveryType DeliveryType { get; set; }
        public virtual ICollection<OrderDeliveryDisplayHeadMap> OrderDeliveryDisplayHeadMaps { get; set; }
        public virtual ReceivingMethod ReceivingMethod { get; set; }
        public virtual ReturnMethod ReturnMethod { get; set; }
        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        public virtual ICollection<ShoppingCart1> ShoppingCarts { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
        public virtual ICollection<OrderContactMap> OrderContactMaps { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Branch Branch1 { get; set; }
        public virtual Company Company { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual DocumentReferenceStatusMap DocumentReferenceStatusMap { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<TransactionHead> TransactionHead1 { get; set; }
        public virtual TransactionHead TransactionHead2 { get; set; }
        public virtual TransactionStatus TransactionStatus { get; set; }
        public virtual ICollection<TransactionHeadAccountMap> TransactionHeadAccountMaps { get; set; }
        public virtual ICollection<TransactionHeadEntitlementMap> TransactionHeadEntitlementMaps { get; set; }
        public virtual ICollection<TransactionHeadPayablesMap> TransactionHeadPayablesMaps { get; set; }
        public virtual ICollection<TransactionHeadPointsMap> TransactionHeadPointsMaps { get; set; }
        public virtual ICollection<TransactionHeadReceivablesMap> TransactionHeadReceivablesMaps { get; set; }
        public virtual ICollection<TransactionHeadShoppingCartMap> TransactionHeadShoppingCartMaps { get; set; }
        public virtual ICollection<TransactionShipment> TransactionShipments { get; set; }
    }
}
