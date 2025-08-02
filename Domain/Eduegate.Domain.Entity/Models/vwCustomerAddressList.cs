using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwCustomerAddressList
    {
        [Key]
        public long AddressID { get; set; }
        public long RefCustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long RefCountryID { get; set; }
        public long RefAreaID { get; set; }
        public string Block { get; set; }
        public string Street { get; set; }
        public string BuildingNo { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string OtherDetails { get; set; }
        public bool AddressDefault { get; set; }
        public string Floor { get; set; }
        public string Flat { get; set; }
        public string CountryCode { get; set; }
        public string CountryNameEn { get; set; }
        public bool CountryActive { get; set; }
        public Nullable<long> AreaID { get; set; }
        public string AreaNameEn { get; set; }
        public Nullable<bool> AreaActive { get; set; }
        public string GroupType { get; set; }
        public string CountryNameAr { get; set; }
        public string AreaNameAr { get; set; }
        public string Jadda { get; set; }
        public string Telephone2 { get; set; }
    }
}
