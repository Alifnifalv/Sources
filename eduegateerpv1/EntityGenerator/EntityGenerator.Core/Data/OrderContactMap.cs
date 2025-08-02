using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OrderContactMaps", Schema = "orders")]
    [Index("IsShippingAddress", Name = "IDX_OrderContactMaps_IsShippingAddress_OrderID")]
    [Index("OrderID", Name = "IDX_OrderContactMaps_OrderID_")]
    [Index("OrderID", Name = "IDX_OrderContactMaps_OrderID_TitleID__FirstName__MiddleName__LastName__Description__BuildingNo__Flo")]
    public partial class OrderContactMap
    {
        public OrderContactMap()
        {
            JobEntryHeads = new HashSet<JobEntryHead>();
        }

        [Key]
        public long OrderContactMapIID { get; set; }
        public long? OrderID { get; set; }
        public short? TitleID { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string MiddleName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        [StringLength(75)]
        public string Description { get; set; }
        [StringLength(75)]
        public string BuildingNo { get; set; }
        [StringLength(75)]
        public string Floor { get; set; }
        [StringLength(75)]
        public string Flat { get; set; }
        [StringLength(75)]
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
        [StringLength(75)]
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
        [StringLength(75)]
        public string AlternateEmailID1 { get; set; }
        [StringLength(75)]
        public string AlternateEmailID2 { get; set; }
        [StringLength(100)]
        public string WebsiteURL1 { get; set; }
        [StringLength(100)]
        public string WebsiteURL2 { get; set; }
        public bool? IsBillingAddress { get; set; }
        public bool? IsShippingAddress { get; set; }
        [StringLength(250)]
        public string SpecialInstruction { get; set; }
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
        [StringLength(75)]
        public string Avenue { get; set; }
        public long? ContactID { get; set; }
        [StringLength(200)]
        public string District { get; set; }
        [StringLength(200)]
        public string LandMark { get; set; }
        public int? CityID { get; set; }
        public int? LocationID { get; set; }
        [StringLength(400)]
        public string LocationName { get; set; }
        public int? LandmarkId { get; set; }

        [ForeignKey("AreaID")]
        [InverseProperty("OrderContactMaps")]
        public virtual Area Area { get; set; }
        [ForeignKey("ContactID")]
        [InverseProperty("OrderContactMaps")]
        public virtual Contact Contact { get; set; }
        [ForeignKey("OrderID")]
        [InverseProperty("OrderContactMaps")]
        public virtual TransactionHead Order { get; set; }
        [ForeignKey("TitleID")]
        [InverseProperty("OrderContactMaps")]
        public virtual Title Title { get; set; }
        [InverseProperty("OrderContactMap")]
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
