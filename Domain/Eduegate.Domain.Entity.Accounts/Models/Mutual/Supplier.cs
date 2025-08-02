using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using Eduegate.Domain.Entity.Accounts.Models.Assets;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Mutual
{
    [Table("Suppliers", Schema = "mutual")]
    public partial class Supplier
    {
        public Supplier()
        {
            AssetSerialMaps = new HashSet<AssetSerialMap>();
            AssetTransactionSerialMaps = new HashSet<AssetTransactionSerialMap>();
            //CustomerSupplierMaps = new HashSet<CustomerSupplierMap>();
            //RFQSupplierRequestMaps = new HashSet<RFQSupplierRequestMap>();
            SupplierAccountMaps = new HashSet<SupplierAccountMap>();
            //SupplierContentIDs = new HashSet<SupplierContentID>();
            //Tickets = new HashSet<Ticket>();
            //TransactionHeads = new HashSet<TransactionHead>();
            //TransactionShipmentSupplierIDFromNavigations = new HashSet<TransactionShipment>();
            //TransactionShipmentSupplierIDToNavigations = new HashSet<TransactionShipment>();
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

        public DateTime? CRExpiry { get; set; }

        [StringLength(255)]
        public string VendorNickName { get; set; }

        [StringLength(255)]
        public string CompanyLocation { get; set; }

        public byte? StatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public long? EmployeeID { get; set; }

        public bool? IsMarketPlace { get; set; }

        public long? BranchID { get; set; }

        public long? BlockedBranchID { get; set; }

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

        public DateTime? CRStartDate { get; set; }

        public string TINNumber { get; set; }

        public int? TaxJurisdictionCountryID { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string DUNSNumber { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string LicenseNumber { get; set; }

        public DateTime? LicenseStartDate { get; set; }

        public DateTime? LicenseExpiryDate { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string EstIDNumber { get; set; }

        public DateTime? EstFirstIssueDate { get; set; }

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

        //public virtual Branch BlockedBranch { get; set; }

        //public virtual BusinessType BusinessType { get; set; }

        //public virtual Company Company { get; set; }

        //public virtual Employee Employee { get; set; }

        //public virtual Login Login { get; set; }

        //public virtual ReceivingMethod ReceivingMethod { get; set; }

        //public virtual ReturnMethod ReturnMethod { get; set; }

        //public virtual SupplierStatus Status { get; set; }

        public virtual Country TaxJurisdictionCountry { get; set; }

        public virtual ICollection<AssetSerialMap> AssetSerialMaps { get; set; }
        public virtual ICollection<AssetTransactionSerialMap> AssetTransactionSerialMaps { get; set; }

        //public virtual ICollection<CustomerSupplierMap> CustomerSupplierMaps { get; set; }

        //public virtual ICollection<RFQSupplierRequestMap> RFQSupplierRequestMaps { get; set; }

        public virtual ICollection<SupplierAccountMap> SupplierAccountMaps { get; set; }

        //public virtual ICollection<SupplierContentID> SupplierContentIDs { get; set; }

        //public virtual ICollection<Ticket> Tickets { get; set; }

        //public virtual ICollection<TransactionHead> TransactionHeads { get; set; }

        //public virtual ICollection<TransactionShipment> TransactionShipmentSupplierIDFromNavigations { get; set; }

        //public virtual ICollection<TransactionShipment> TransactionShipmentSupplierIDToNavigations { get; set; }
    }
}