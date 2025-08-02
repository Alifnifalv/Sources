using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.HR.Employee
{
    [DataContract]
    public class ContactsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ContactID { get; set; }

        [DataMember]
        public Nullable<short> TitleID { get; set; }

        [DataMember]
        public Nullable<long> SupplierID { get; set; }

        [DataMember]
        public Nullable<long> CustomerID { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public Nullable<long> LoginID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Nullable<long> CountryID { get; set; }

        [DataMember]
        public string CountryName { get; set; }

        [DataMember]
        public string Block { get; set; }

        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public string BuildingNo { get; set; }

        [DataMember]
        public string Floor { get; set; }

        [DataMember]
        public string Flat { get; set; }

        [DataMember]
        public string MobileNo1 { get; set; }

        [DataMember]
        public string MobileNo2 { get; set; }

        [DataMember]
        public string PhoneNo1 { get; set; }

        [DataMember]
        public string PhoneNo2 { get; set; }

        [DataMember]
        public string OfficePhoneNo { get; set; }

        [DataMember]
        public string AddressName { get; set; }

        [DataMember]
        public string AddressLine1 { get; set; }

        [DataMember]
        public string AddressLine2 { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string PassportNumber { get; set; }

        [DataMember]
        public string CivilIDNumber { get; set; }

        [DataMember]
        public Nullable<long> PassportIssueCountryID { get; set; }

        [DataMember]
        public string TelephoneCode { get; set; }

        [DataMember]
        public string AlternateEmailID1 { get; set; }

        [DataMember]
        public string AlternateEmailID2 { get; set; }

        [DataMember]
        public string WebsiteURL1 { get; set; }

        [DataMember]
        public string WebsiteURL2 { get; set; }

        [DataMember]
        public string TelephoneNumber { get; set; }

        [DataMember]
        public Nullable<bool> IsBillingAddress { get; set; }

        [DataMember]
        public Nullable<bool> IsShippingAddress { get; set; }

        [DataMember]
        public KeyValueDTO Country { get; set; }

        [DataMember]
        public Nullable<int> AreaID { get; set; }

        [DataMember]
        public string Avenue { get; set; }

        //[DataMember]
        //public List<EntityPropertyMapDTO> Phones { get; set; }

        //[DataMember]
        //public List<EntityPropertyMapDTO> Emails { get; set; }

        //[DataMember]
        //public List<EntityPropertyMapDTO> Faxs { get; set; }

        [DataMember]
        public KeyValueDTO Areas { get; set; }

        [DataMember]
        public string AreaName { get; set; }

        [DataMember]
        public int CityID { get; set; }

        [DataMember]
        public string District { get; set; }

        [DataMember]
        public string LandMark { get; set; }

        [DataMember]
        public KeyValueDTO Cities { get; set; }

        [DataMember]
        public string IntlCity { get; set; }

        [DataMember]
        public string IntlArea { get; set; }

        [DataMember]
        public int? StatusID { get; set; }

        [DataMember]
        public KeyValueDTO Status { get; set; }
    }
}
