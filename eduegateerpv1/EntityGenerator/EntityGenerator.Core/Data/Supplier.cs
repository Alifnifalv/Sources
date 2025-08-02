using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Suppliers", Schema = "mutual")]
    public partial class Supplier
    {
        public Supplier()
        {
            CustomerSupplierMaps = new HashSet<CustomerSupplierMap>();
            RFQSupplierRequestMaps = new HashSet<RFQSupplierRequestMap>();
            SupplierAccountMaps = new HashSet<SupplierAccountMap>();
            SupplierContentIDs = new HashSet<SupplierContentID>();
            Tickets = new HashSet<Ticket>();
            TransactionHeads = new HashSet<TransactionHead>();
            TransactionShipmentSupplierIDFromNavigations = new HashSet<TransactionShipment>();
            TransactionShipmentSupplierIDToNavigations = new HashSet<TransactionShipment>();
        }

        [Key]
        public long SupplierIID { get; set; }
        public int? CompanyID { get; set; }
        public long? LoginID { get; set; }
        [StringLength(50)]
        public string SupplierCode { get; set; }
        public long? TitleID { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string VendorCR { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CRExpiry { get; set; }
        [StringLength(255)]
        public string VendorNickName { get; set; }
        [StringLength(255)]
        public string CompanyLocation { get; set; }
        public byte? StatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? EmployeeID { get; set; }
        public bool? IsMarketPlace { get; set; }
        public long? BranchID { get; set; }
        public long? BlockedBranchID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Profit { get; set; }
        public int? AliasID { get; set; }
        public int? ReturnMethodID { get; set; }
        public int? ReceivingMethodID { get; set; }
        [StringLength(50)]
        public string Telephone { get; set; }
        [StringLength(100)]
        public string SupplierEmail { get; set; }
        [StringLength(500)]
        public string SupplierAddress { get; set; }
        public string FaxNumber { get; set; }
        public string WebsiteURL { get; set; }
        public long? BusinessTypeID { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string YearEstablished { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CRStartDate { get; set; }
        public string TINNumber { get; set; }
        public int? TaxJurisdictionCountryID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string DUNSNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string LicenseNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LicenseStartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LicenseExpiryDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string EstIDNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EstFirstIssueDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EstExpiryDate { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string AccountNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string IBAN { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string SWIFT_BIC_Code { get; set; }
        public bool? IsCreditReference { get; set; }
        public int? PaymentMaxNoOfDaysAllowed { get; set; }
        public string ProductorServiceDescription { get; set; }
        public string PricingInformation { get; set; }
        public int? LeadTimeDays { get; set; }
        public int? MinOrderQty { get; set; }
        public string Warranty_GuaranteeInfo { get; set; }
        public string NamesOfClients { get; set; }
        public string ClientContactInformation { get; set; }
        public string ClientProjectDetails { get; set; }
        public string PrevContractScopeOfWork { get; set; }
        public string PrevValueOfContracts { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PrevContractDuration { get; set; }
        public string CommunicationAddress { get; set; }
        public string PhysicalAddress { get; set; }

        [ForeignKey("BlockedBranchID")]
        [InverseProperty("Suppliers")]
        public virtual Branch BlockedBranch { get; set; }
        [ForeignKey("BusinessTypeID")]
        [InverseProperty("Suppliers")]
        public virtual BusinessType BusinessType { get; set; }
        [ForeignKey("CompanyID")]
        [InverseProperty("Suppliers")]
        public virtual Company Company { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("Suppliers")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("Suppliers")]
        public virtual Login Login { get; set; }
        [ForeignKey("ReceivingMethodID")]
        [InverseProperty("Suppliers")]
        public virtual ReceivingMethod ReceivingMethod { get; set; }
        [ForeignKey("ReturnMethodID")]
        [InverseProperty("Suppliers")]
        public virtual ReturnMethod ReturnMethod { get; set; }
        [ForeignKey("StatusID")]
        [InverseProperty("Suppliers")]
        public virtual SupplierStatus Status { get; set; }
        [ForeignKey("TaxJurisdictionCountryID")]
        [InverseProperty("Suppliers")]
        public virtual Country TaxJurisdictionCountry { get; set; }
        [InverseProperty("Supplier")]
        public virtual ICollection<CustomerSupplierMap> CustomerSupplierMaps { get; set; }
        [InverseProperty("Supplier")]
        public virtual ICollection<RFQSupplierRequestMap> RFQSupplierRequestMaps { get; set; }
        [InverseProperty("Supplier")]
        public virtual ICollection<SupplierAccountMap> SupplierAccountMaps { get; set; }
        [InverseProperty("Supplier")]
        public virtual ICollection<SupplierContentID> SupplierContentIDs { get; set; }
        [InverseProperty("Supplier")]
        public virtual ICollection<Ticket> Tickets { get; set; }
        [InverseProperty("Supplier")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        [InverseProperty("SupplierIDFromNavigation")]
        public virtual ICollection<TransactionShipment> TransactionShipmentSupplierIDFromNavigations { get; set; }
        [InverseProperty("SupplierIDToNavigation")]
        public virtual ICollection<TransactionShipment> TransactionShipmentSupplierIDToNavigations { get; set; }
    }
}
