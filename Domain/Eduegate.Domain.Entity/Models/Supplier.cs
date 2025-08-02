using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Suppliers", Schema = "mutual")]
    public partial class Supplier
    {
        public Supplier()
        {
            this.ProductPriceListSupplierMaps = new List<ProductPriceListSupplierMap>();
            this.SupplierAccountMaps = new List<SupplierAccountMap>();
            this.Tickets = new List<Ticket>();
            this.TransactionHeads = new List<TransactionHead>();
            this.TransactionShipments = new List<TransactionShipment>();
            this.TransactionShipments1 = new List<TransactionShipment>();
            this.Contacts = new List<Contact>();
            this.CustomerSupplierMaps = new List<CustomerSupplierMap>();
            RFQSupplierRequestMaps = new HashSet<RFQSupplierRequestMap>();
            SupplierContentIDs = new HashSet<SupplierContentIDs>();
        }

        [Key]
        public long SupplierIID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<long> LoginID { get; set; }
        public Nullable<long> TitleID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string VendorCR { get; set; }
        public string Telephone { get; set; }
        public string SupplierEmail { get; set; }
        public DateTime? CRExpiry { get; set; }
        public string VendorNickName { get; set; }
        public string CompanyLocation { get; set; }
        public Nullable<byte> StatusID { get; set; }

        [StringLength(50)]
        public string SupplierCode { get; set; }

        [StringLength(500)]
        public string SupplierAddress { get; set; }
        public string CommunicationAddress { get; set; }
        public string PhysicalAddress { get; set; }
        public string WebsiteURL { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public Nullable<bool> IsMarketPlace { get; set; }
        public Nullable<long> BranchID { get; set; }
        public Nullable<long> BlockedBranchID { get; set; }


        //New Added fields for modification
        public long? BusinessTypeID { get; set; }
        public string YearEstablished { get; set; }
        public DateTime? CRStartDate { get; set; }
        public string TINNumber { get; set; }
        public int? TaxJurisdictionCountryID { get; set; }
        public string DUNSNumber { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime? LicenseStartDate { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public string EstIDNumber { get; set; }
        public DateTime? EstFirstIssueDate { get; set; }
        public DateTime? EstExpiryDate { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string AccountNumber { get; set; }
        public string IBAN { get; set; }
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
        public string PrevContractDuration { get; set; }



        public virtual ICollection<SupplierAccountMap> SupplierAccountMaps { get; set; }
        public virtual Login Login { get; set; }
        public virtual ICollection<ProductPriceListSupplierMap> ProductPriceListSupplierMaps { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual ICollection<TransactionShipment> TransactionShipments { get; set; }
        public virtual ICollection<TransactionShipment> TransactionShipments1 { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<CustomerSupplierMap> CustomerSupplierMaps { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual SupplierStatus SupplierStatus { get; set; }
        public Nullable<Decimal> Profit { get; set; }
        public Nullable<int> ReturnMethodID { get; set; }
        public Nullable<int> ReceivingMethodID { get; set; }
        public virtual ReceivingMethod ReceivingMethod { get; set; }
        public virtual ReturnMethod ReturnMethod { get; set; }

        public virtual ICollection<RFQSupplierRequestMap> RFQSupplierRequestMaps { get; set; }

        public virtual Country TaxJurisdictionCountry { get; set; }
        public virtual BusinessTypes BusinessType { get; set; }

        public virtual ICollection<SupplierContentIDs> SupplierContentIDs { get; set; }
    }
}
