namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.Areas")]
    public partial class Area
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Area()
        {
            DeliveryTypeAllowedAreaMaps = new HashSet<DeliveryTypeAllowedAreaMap>();
            AreaCultureDatas = new HashSet<AreaCultureData>();
            Areas1 = new HashSet<Area>();
            Locations1 = new HashSet<Locations1>();
            OrderContactMaps = new HashSet<OrderContactMap>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AreaID { get; set; }

        [StringLength(50)]
        public string AreaName { get; set; }

        public short? ZoneID { get; set; }

        public int? CityID { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? RouteID { get; set; }

        public int? CountryID { get; set; }

        public int? CompanyID { get; set; }

        public int? ParentAreaID { get; set; }

        [StringLength(50)]
        public string ExternalCode { get; set; }

        public decimal? MinimumCapAmount { get; set; }

        public long? BranchID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypeAllowedAreaMap> DeliveryTypeAllowedAreaMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AreaCultureData> AreaCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Area> Areas1 { get; set; }

        public virtual Area Area1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Locations1> Locations1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderContactMap> OrderContactMaps { get; set; }
    }
}
