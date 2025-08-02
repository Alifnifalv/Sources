using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CustomerSearchView
    {
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public long CustomerIID { get; set; }
        [StringLength(7)]
        [Unicode(false)]
        public string IsofflineCustomer { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string Telephone { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [StringLength(100)]
        public string LoginEmailID { get; set; }
        [StringLength(200)]
        public string CustomerEmail { get; set; }
        [StringLength(50)]
        public string CustomerCR { get; set; }
        [StringLength(767)]
        public string Customer { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string MiddleName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string BuildingNo { get; set; }
        [StringLength(20)]
        public string Floor { get; set; }
        [StringLength(20)]
        public string Flat { get; set; }
        [StringLength(500)]
        public string AddressLine1 { get; set; }
        [StringLength(500)]
        public string AddressLine2 { get; set; }
        [StringLength(100)]
        public string State { get; set; }
        [StringLength(100)]
        public string PostalCode { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(50)]
        public string Block { get; set; }
        public long? CountryID { get; set; }
        [StringLength(50)]
        public string Street { get; set; }
        [StringLength(50)]
        public string TelephoneCode { get; set; }
        [StringLength(50)]
        public string MobileNo1 { get; set; }
        [StringLength(20)]
        public string MobileNo2 { get; set; }
        [StringLength(50)]
        public string PhoneNo1 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastLoginDate { get; set; }
        public byte? StatusID { get; set; }
        [StringLength(399)]
        public string AddressName { get; set; }
        [StringLength(50)]
        public string GroupName { get; set; }
        [StringLength(767)]
        public string ContactPerson { get; set; }
        [StringLength(258)]
        public string ProductOwner { get; set; }
        public string Entitlements { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? companyid { get; set; }
    }
}
