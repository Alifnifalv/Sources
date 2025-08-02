namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.Locations")]
    public partial class Location
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Location()
        {
            ProductLocationMaps = new HashSet<ProductLocationMap>();
        }

        [Key]
        public long LocationIID { get; set; }

        [StringLength(50)]
        public string LocationCode { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public long? BranchID { get; set; }

        public byte? LocationTypeID { get; set; }

        [StringLength(50)]
        public string Barcode { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual LocationType LocationType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductLocationMap> ProductLocationMaps { get; set; }
    }
}
