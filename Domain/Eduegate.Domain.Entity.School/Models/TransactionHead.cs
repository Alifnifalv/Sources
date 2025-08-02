namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TransactionHead", Schema = "inventory")]
    public partial class TransactionHead
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransactionHead()
        {
            //OrderDeliveryDisplayHeadMaps = new HashSet<OrderDeliveryDisplayHeadMap>();
            //InvetoryTransactions = new HashSet<InvetoryTransaction>();
            //ShoppingCarts = new HashSet<ShoppingCart1>();
            //TaxTransactions = new HashSet<TaxTransaction>();
            //JobEntryHeads = new HashSet<JobEntryHead>();
            //OrderContactMaps = new HashSet<OrderContactMap>();
            //OrderRoleTrackings = new HashSet<OrderRoleTracking>();
            //TransactionHead1 = new HashSet<TransactionHead>();
            //TransactionHeadAccountMaps = new HashSet<TransactionHeadAccountMap>();
            //TransactionHeadEntitlementMaps = new HashSet<TransactionHeadEntitlementMap>();
            //TransactionHeadPayablesMaps = new HashSet<TransactionHeadPayablesMap>();
            //TransactionHeadPointsMaps = new HashSet<TransactionHeadPointsMap>();
            //TransactionHeadReceivablesMaps = new HashSet<TransactionHeadReceivablesMap>();
            //TransactionHeadShoppingCartMaps = new HashSet<TransactionHeadShoppingCartMap>();
            //TransactionShipments = new HashSet<TransactionShipment>();
            //WorkflowTransactionHeadMaps = new HashSet<WorkflowTransactionHeadMap>();
        }

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

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public decimal? DeliveryCharge { get; set; }

        public int? DeliveryTypeID { get; set; }

        public int? JobStatusID { get; set; }

        public long? DocumentStatusID { get; set; }

        public int? TransactionRole { get; set; }

        [StringLength(500)]
        public string Reference { get; set; }

        public DateTime? DocumentCancelledDate { get; set; }

        public int? ReceivingMethodID { get; set; }

        public int? ReturnMethodID { get; set; }

        public int? DeliveryTimeslotID { get; set; }

        public decimal? ToalAmount { get; set; }

        public decimal? ForeignAmount { get; set; }
        public decimal? TotalLandingCost { get; set; }

        public decimal? InvoiceLocalAmount { get; set; }
        public decimal? InvoiceForeignAmount { get; set; }

        public decimal? LocalDiscount { get; set; }

        [StringLength(50)]
        public string ExternalReference1 { get; set; }

        [StringLength(50)]
        public string ExternalReference2 { get; set; }

        //public virtual Role Role { get; set; }

        //public virtual DeliveryType DeliveryType { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<OrderDeliveryDisplayHeadMap> OrderDeliveryDisplayHeadMaps { get; set; }

        //public virtual ReceivingMethod ReceivingMethod { get; set; }

        //public virtual ReturnMethod ReturnMethod { get; set; }

        //public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ShoppingCart1> ShoppingCarts { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<OrderContactMap> OrderContactMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<OrderRoleTracking> OrderRoleTrackings { get; set; }

        //public virtual Branch Branch { get; set; }

        //public virtual Branch Branch1 { get; set; }

        public virtual Company Company { get; set; }

        //public virtual Currency Currency { get; set; }

        //public virtual Customer Customer { get; set; }

        public virtual DocumentReferenceStatusMap DocumentReferenceStatusMap { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual Employee Employee { get; set; }

        //public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }

        //public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHead> TransactionHead1 { get; set; }

        public virtual TransactionHead TransactionHead2 { get; set; }

        public virtual TransactionStatus TransactionStatus { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TransactionHeadAccountMap> TransactionHeadAccountMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TransactionHeadEntitlementMap> TransactionHeadEntitlementMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TransactionHeadPayablesMap> TransactionHeadPayablesMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TransactionHeadPointsMap> TransactionHeadPointsMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TransactionHeadReceivablesMap> TransactionHeadReceivablesMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TransactionHeadShoppingCartMap> TransactionHeadShoppingCartMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TransactionShipment> TransactionShipments { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<WorkflowTransactionHeadMap> WorkflowTransactionHeadMaps { get; set; }
    }
}
