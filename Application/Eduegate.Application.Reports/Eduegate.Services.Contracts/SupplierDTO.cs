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

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class SupplierDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long SupplierIID { get; set; }

        [DataMember]
        public Nullable<long> LoginID { get; set; }

        [DataMember]
        public Nullable<short> TitleID { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public Nullable<byte> StatusID { get; set; }

        [DataMember]
        public List<ContactDTO> Contacts { get; set; }
        [DataMember]
        public long EmployeeID { get; set; }
        [DataMember]
        public string VendorCR { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CRExpiry { get; set; }
        [DataMember]
        public string VendorNickName { get; set; }
        [DataMember]
        public string CompanyLocation { get; set; }
        [DataMember]
        public string SupplierEmail { get; set; }
        [DataMember]
        public string TelephoneNumber { get; set; }

        [DataMember]
        public bool IsCheque { get; set; }
        [DataMember]
        public Nullable<long> ChequeTypeID { get; set; }
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
        public List<BankAccountDTO> BankAccounts { get; set; }
        [DataMember]
        public ExternalSettingsDTO ExternalSettings { get; set; }

        // Document
        [DataMember]
        public DocumentViewDTO Document { get; set; }

        [DataMember]
        public LoginDTO Login { get; set; }

        [DataMember]
        public Nullable<bool> IsMarketPlace { get; set; }
        [DataMember]
        public Nullable<long> BranchID { get; set; }
        [DataMember]
        public Nullable<long> ReceivingMethodID { get; set; }

        [DataMember]
        public Nullable<long> ReturnMethodID { get; set; }
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
        public Nullable<decimal> Profit { get; set; }

        [DataMember]
        public KeyValueDTO GLAccountID { get; set; }

        [DataMember]
        public string SupplierCode { get; set; }

        [DataMember]
        public string SupplierAddress { get; set; }

    }
}