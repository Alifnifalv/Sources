using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class SupplierDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SupplierDTO() {
            Contacts = new List<ContactDTO>();
            BusinessDetail = new BusinessDetailDTO();
            BankAccounts = new BankAccountDTO();
            ExternalSettings = new ExternalSettingsDTO();
            Document = new DocumentViewDTO();
        }

        [DataMember]
        public long SupplierIID { get; set; }

        [DataMember]
        public string SupplierCode { get; set; }

        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string VendorNickName { get; set; }

        [DataMember]
        public string CommunicationAddress { get; set; }

        [DataMember]
        public string PhysicalAddress { get; set; }

        [DataMember]
        public string WebsiteURL { get; set; }

        [DataMember]
        public string TelephoneNumber { get; set; }

        [DataMember]
        public string SupplierEmail { get; set; }


        //Contacts Tab
        [DataMember]
        public List<ContactDTO> Contacts { get; set; }


        //Business Detail
        [DataMember]
        public BusinessDetailDTO BusinessDetail { get; set; }

        //Bank Accounts
        [DataMember]
        public BankAccountDTO BankAccounts { get; set; }

        //Product/Service Information
        [DataMember]
        public ExternalSettingsDTO ExternalSettings { get; set; }

        //Complians and Certifications
        // Document
        [DataMember]
        public DocumentViewDTO Document { get; set; }


        //Reference and Past Performance

        [DataMember]
        public string NamesOfClients { get; set; }

        [DataMember]
        public string ClientContactInformation {  get; set; }
        
        [DataMember]
        public string ClientProjectDetails { get; set; }

        [DataMember]
        public string PrevContractScopeOfWork { get; set; }

        [DataMember]
        public string PrevValueOfContracts { get; set; }

        [DataMember]
        public string PrevContractDuration { get; set; }

        [DataMember]
        public bool? Declaration { get; set; }

        [DataMember]
        public byte? StatusID { get; set; }


        [DataMember]
        public short? TitleID { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public long EmployeeID { get; set; }

        [DataMember]
        public string CompanyLocation { get; set; }

        [DataMember]
        public bool IsCheque { get; set; }
        [DataMember]
        public long? ChequeTypeID { get; set; }
        [DataMember]
        public string ChequeName { get; set; }
        [DataMember]
        public bool IsBankAccount { get; set; }
        [DataMember]
        public bool IsCash { get; set; }

        // For getting Employee Id and Name 
        [DataMember]
        public List<KeyValueDTO> KeyValueEmployees { get; set; }

        [DataMember]
        public EntitlementDTO Entitlements { get; set; }


        [DataMember]
        public LoginDTO Login { get; set; }

        [DataMember]
        public bool? IsMarketPlace { get; set; }

        [DataMember]
        public long? BranchID { get; set; }

        [DataMember]
        public long? ReceivingMethodID { get; set; }

        [DataMember]
        public long? ReturnMethodID { get; set; }

        [DataMember]
        public PriceListDetailDTO PriceLists { get; set; }

        [DataMember]
        public SupplierAccountMapDTO SupplierAccountMaps { get; set; }

        [DataMember]
        public int? CompanyID { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public string ReceivingMethodName { get; set; }

        [DataMember]
        public string ReturnMethodName { get; set; }

        [DataMember]
        public decimal? Profit { get; set; }

        [DataMember]
        public KeyValueDTO GLAccountID { get; set; }


        [DataMember]
        public string SupplierAddress { get; set; }


    }
}