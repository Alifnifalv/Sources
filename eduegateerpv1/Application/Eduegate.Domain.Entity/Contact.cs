namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.Contacts")]
    public partial class Contact
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lead> Leads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderContactMap> OrderContactMaps { get; set; }
    }
}
