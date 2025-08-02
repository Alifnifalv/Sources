using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SupplierSearchView
    {
        [StringLength(50)]
        public string Description { get; set; }
        [StringLength(50)]
        public string BuildingNo { get; set; }
        [StringLength(20)]
        public string Floor { get; set; }
        [StringLength(20)]
        public string Flat { get; set; }
        [StringLength(50)]
        public string Block { get; set; }
        [StringLength(500)]
        public string AddressName { get; set; }
        [StringLength(500)]
        public string AddressLine1 { get; set; }
        [StringLength(500)]
        public string AddressLine2 { get; set; }
        [StringLength(100)]
        public string State { get; set; }
        public long? CountryID { get; set; }
        [StringLength(100)]
        public string PostalCode { get; set; }
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
        [StringLength(50)]
        public string PhoneNo2 { get; set; }
        public long? PassportIssueCountryID { get; set; }
        [StringLength(50)]
        public string AlternateEmailID1 { get; set; }
        [StringLength(50)]
        public string AlternateEmailID2 { get; set; }
        [StringLength(100)]
        public string WebsiteURL1 { get; set; }
        [StringLength(100)]
        public string WebsiteURL2 { get; set; }
        [StringLength(100)]
        public string LoginEmailID { get; set; }
        public byte? StatusID { get; set; }
        public long? TitleID { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(255)]
        public string MiddleName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        public long SupplierIID { get; set; }
    }
}
