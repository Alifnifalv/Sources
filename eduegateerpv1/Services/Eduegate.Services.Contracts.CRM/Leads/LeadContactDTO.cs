using Eduegate.Framework.Contracts.Common;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Leads
{
    [DataContract]
    public class LeadContactDTO : BaseMasterDTO
    {
        [DataMember]
        public long ContactIID { get; set; }

        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public long? TitleID { get; set; }

        [DataMember]
        public long? SupplierID { get; set; }

        [DataMember]
        public long? CustomerID { get; set; }

        [DataMember]
        [StringLength(255)]
        public string FirstName { get; set; }

        [DataMember]
        [StringLength(255)]
        public string MiddleName { get; set; }

        [DataMember]
        [StringLength(255)]
        public string LastName { get; set; }

        [DataMember]
        [StringLength(50)]
        public string Description { get; set; }

        [DataMember]
        [StringLength(50)]
        public string BuildingNo { get; set; }

        [DataMember]
        [StringLength(20)]
        public string Floor { get; set; }

        [DataMember]
        [StringLength(20)]
        public string Flat { get; set; }

        [DataMember]
        [StringLength(50)]
        public string Block { get; set; }

        [DataMember]
        [StringLength(500)]
        public string AddressName { get; set; }

        [DataMember]
        [StringLength(500)]
        public string AddressLine1 { get; set; }

        [DataMember]
        [StringLength(500)]
        public string AddressLine2 { get; set; }

        [DataMember]
        [StringLength(100)]
        public string State { get; set; }

        [DataMember]
        [StringLength(100)]
        public string City { get; set; }

        [DataMember]
        public long? CountryID { get; set; }

        [DataMember]
        [StringLength(100)]
        public string PostalCode { get; set; }

        [DataMember]
        [StringLength(50)]
        public string Street { get; set; }

        [DataMember]
        [StringLength(50)]
        public string TelephoneCode { get; set; }

        [DataMember]
        [StringLength(50)]
        public string MobileNo1 { get; set; }

        [DataMember]
        [StringLength(20)]
        public string MobileNo2 { get; set; }

        [DataMember]
        [StringLength(50)]
        public string PhoneNo1 { get; set; }

        [DataMember]
        [StringLength(50)]
        public string PhoneNo2 { get; set; }

        [DataMember]
        [StringLength(100)]
        public string PassportNumber { get; set; }

        [DataMember]
        [StringLength(100)]
        public string CivilIDNumber { get; set; }

        [DataMember]
        public long? PassportIssueCountryID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string AlternateEmailID1 { get; set; }

        [DataMember]
        [StringLength(50)]
        public string AlternateEmailID2 { get; set; }

        [DataMember]
        [StringLength(100)]
        public string WebsiteURL1 { get; set; }

        [DataMember]
        [StringLength(100)]
        public string WebsiteURL2 { get; set; }

        [DataMember]
        public bool? IsBillingAddress { get; set; }

        [DataMember]
        public bool? IsShippingAddress { get; set; }

        [DataMember]
        public decimal? Latitude { get; set; }

        [DataMember]
        public decimal? Longitude { get; set; }
    }
}


