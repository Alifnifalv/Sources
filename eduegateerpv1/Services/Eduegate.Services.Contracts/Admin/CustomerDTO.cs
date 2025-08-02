using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Admin;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class CustomerDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long CustomerIID { get; set; }

        [DataMember]
        public Nullable<long> LoginID { get; set; }

        [DataMember]
        public Nullable<int> GroupID { get; set; }

        [DataMember]
        public Nullable<short> TitleID { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public Nullable<bool> IsDifferentBillingAddress { get; set; }

        [DataMember]
        public Nullable<bool> IsTermsAndConditions { get; set; }

        [DataMember]
        public Nullable<bool> IsSubscribeOurNewsLetter { get; set; }

        [DataMember]
        public string TelephoneCode { get; set; }

        [DataMember]
        public string TelephoneNumber { get; set; }

        [DataMember]
        public Nullable<long> CountryID { get; set; }

        [DataMember]
        public string PassportNumber { get; set; }

        [DataMember]
        public string CivilIDNumber { get; set; }

        [DataMember]
        public Nullable<long> PassportIssueCountryID { get; set; }

        [DataMember]
        public bool IsPassword { get; set; }
        
        [DataMember]
        public Nullable<long> StatusID { get; set; }

        [DataMember]
        public Nullable<byte> GenderID { get; set; }

        [DataMember]
        public CustomerSettingDTO Settings { get; set; }

        [DataMember]
        public List<ContactDTO> Contacts { get; set; }
        [DataMember]
        public LoginDTO Login { get; set; }
        [DataMember]
        public PropertyDTO Property { get; set; }

        [DataMember]
        public Nullable<bool> IsOfflineCustomer { get; set; }
        [DataMember]
        public string CustomerCR { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CRExpiryDate { get; set; }
        [DataMember]
        public KeyValueDTO ParentCustomerID { get; set; }

        [DataMember]
        public KeyValueDTO ProductManagerID { get; set; } 

        [DataMember]
        public string ParentFirstName { get; set; }

        [DataMember]
        public string ParentMiddleName { get; set; }

        [DataMember]
        public string ParentLastName { get; set; }

        [DataMember]
        public KeyValueDTO GLAccountID { get; set; }

        [DataMember]
        public KeyValueDTO SupplierID { get; set; }

        [DataMember]
        public EntitlementDTO Entitlements { get; set; }

        [DataMember]
        public PriceListEntitlementDTO PriceListEntitlement { get; set; }

        [DataMember]
        public DocumentViewDTO Document { get; set; }
        [DataMember]
        public ExternalSettingsDTO ExternalSettings { get; set; }
        [DataMember]
        public Nullable<long> HowKnowOptionID { get; set; }
        [DataMember]
        public string HowKnowText { get; set; }

        [DataMember]
        public string CustomerEmail { get; set; }
        [DataMember]
        public string CustomerNumber { get; set; }
        [DataMember]
        public int? CompanyID { get; set; }
        [DataMember]
        public string CustomerCardNumber { get; set; }

        [DataMember]
        public long? DefaultStudentID { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public string StudentProfile { get; set; }
    }
}
