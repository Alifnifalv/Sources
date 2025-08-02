using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("OrderContactMaps", Schema = "orders")]
    public partial class OrderContactMap
    {
        public OrderContactMap()
        {
            JobEntryHeads = new List<JobEntryHead>();
        }

        [Key]
        public long OrderContactMapIID { get; set; }
        public Nullable<long> OrderID { get; set; }
        public Nullable<short> TitleID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public Nullable<int> AreaID { get; set; }
        public string Avenue { get; set; }
        public string BuildingNo { get; set; }
        public string Floor { get; set; }
        public string Flat { get; set; }
        public string Block { get; set; }
        public string AddressName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string LandMark { get; set; } 
        public Nullable<long> CountryID { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string TelephoneCode { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }
        public string PassportNumber { get; set; }
        public string CivilIDNumber { get; set; }
        public Nullable<long> PassportIssueCountryID { get; set; }
        public string AlternateEmailID1 { get; set; }
        public string AlternateEmailID2 { get; set; }
        public string WebsiteURL1 { get; set; }
        public string WebsiteURL2 { get; set; }
        public Nullable<bool> IsBillingAddress { get; set; }
        public Nullable<bool> IsShippingAddress { get; set; }
        public string SpecialInstruction { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        ////public byte[] TimeStamps { get; set; }

        //public string CountryName { get; set; }
        //public string AreaName { get; set; }
        public Nullable<long> ContactID { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
        public virtual Area Area { get; set; }
        public virtual Title Title { get; set; }
        
        public Nullable<int> CityID { get; set; }

        public int? LocationID { get; set; }

        public int? LandmarkId { get; set; }

    }
}
