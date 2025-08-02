using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class OrderContactMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long OrderContactMapID { get; set; }
        [DataMember]
        public long? OrderID { get; set; }
        [DataMember]
        public Nullable<short> TitleID { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string BuildingNo { get; set; }
        [DataMember]
        public string Floor { get; set; }
        [DataMember]
        public string Flat { get; set; }
        [DataMember]
        public string Avenue { get; set; } 
        [DataMember]
        public string Block { get; set; }
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
        public string District { get; set; }
        [DataMember] 
        public string LandMark { get; set; }
        [DataMember]
        public Nullable<long> CountryID { get; set; }
        [DataMember]
        public string PostalCode { get; set; }
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string TelephoneCode { get; set; }
        [DataMember]
        public string MobileNo1 { get; set; }
        [DataMember]
        public string MobileNo2 { get; set; }
        [DataMember]
        public string PhoneNo1 { get; set; }
        [DataMember]
        public string PhoneNo2 { get; set; }
        [DataMember]
        public string PassportNumber { get; set; }
        [DataMember]
        public string CivilIDNumber { get; set; }
        [DataMember]
        public Nullable<long> PassportIssueCountryID { get; set; }
        [DataMember]
        public string AlternateEmailID1 { get; set; }
        [DataMember]
        public string AlternateEmailID2 { get; set; }
        [DataMember]
        public string WebsiteURL1 { get; set; }
        [DataMember]
        public string WebsiteURL2 { get; set; }
        [DataMember]
        public Nullable<bool> IsBillingAddress { get; set; }
        [DataMember]
        public Nullable<bool> IsShippingAddress { get; set; }
        [DataMember]
        public string SpecialInstruction { get; set; }
        [DataMember]
        public Nullable<long> ContactID { get; set; }
        [DataMember]
        public Nullable<int> AreaID { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string AreaName { get; set; }
        [DataMember]
        public int CityId { get; set; }
        [DataMember]
        public string CityName { get; set; }

        [DataMember]
        public string LocationName { get; set; }
    }
}
