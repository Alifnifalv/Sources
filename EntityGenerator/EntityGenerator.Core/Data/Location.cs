using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Locations", Schema = "inventory")]
    public partial class Location
    {
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
        [Unicode(false)]
        public string Barcode { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }

        [ForeignKey("BranchID")]
        [InverseProperty("Locations")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("LocationTypeID")]
        [InverseProperty("Locations")]
        public virtual LocationType LocationType { get; set; }
        [InverseProperty("Location")]
        public virtual ICollection<ProductLocationMap> ProductLocationMaps { get; set; }
    }
}
