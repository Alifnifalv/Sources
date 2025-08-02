using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Contacts", Schema = "mutual")]
    [Index("CustomerID", Name = "IDX_Contacts_CustomerID_")]
    [Index("IsBillingAddress", Name = "IDX_Contacts_IsBillingAddress_LoginID")]
    [Index("IsShippingAddress", Name = "IDX_Contacts_IsShippingAddress_LoginID")]
    [Index("LoginID", "IsBillingAddress", Name = "IDX_Contacts_LoginID__IsBillingAddress_")]
    [Index("LoginID", "IsShippingAddress", Name = "IDX_Contacts_LoginID__IsShippingAddress_")]
    public partial class Contact
    {
        public Contact()
        {
            Leads = new HashSet<Lead>();
            OrderContactMaps = new HashSet<OrderContactMap>();
        }

        [Key]
        public long ContactIID { get; set; }
        public long? LoginID { get; set; }
        public long? TitleID { get; set; }
        public long? SupplierID { get; set; }
        public long? CustomerID { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string MiddleName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
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
        [StringLength(100)]
        public string City { get; set; }
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
        [StringLength(100)]
        public string PassportNumber { get; set; }
        [StringLength(100)]
        public string CivilIDNumber { get; set; }
        public long? PassportIssueCountryID { get; set; }
        [StringLength(50)]
        public string AlternateEmailID1 { get; set; }
        [StringLength(50)]
        public string AlternateEmailID2 { get; set; }
        [StringLength(100)]
        public string WebsiteURL1 { get; set; }
        [StringLength(100)]
        public string WebsiteURL2 { get; set; }
        public bool? IsBillingAddress { get; set; }
        public bool? IsShippingAddress { get; set; }
        [Column(TypeName = "decimal(12, 9)")]
        public decimal? Latitude { get; set; }
        [Column(TypeName = "decimal(12, 9)")]
        public decimal? Longitude { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? AreaID { get; set; }
        [StringLength(20)]
        public string Avenue { get; set; }
        [StringLength(200)]
        public string District { get; set; }
        [StringLength(200)]
        public string LandMark { get; set; }
        public int? CityID { get; set; }
        public int? StatusID { get; set; }
        [StringLength(200)]
        public string IntlCity { get; set; }
        [StringLength(200)]
        public string IntlArea { get; set; }
        public int? LocationID { get; set; }
        [StringLength(400)]
        public string LocationName { get; set; }
        public long? BranchID { get; set; }
        public bool? IsPrimaryContactPerson { get; set; }
        public string Title { get; set; }

        [InverseProperty("Contact")]
        public virtual ICollection<Lead> Leads { get; set; }
        [InverseProperty("Contact")]
        public virtual ICollection<OrderContactMap> OrderContactMaps { get; set; }
    }
}
