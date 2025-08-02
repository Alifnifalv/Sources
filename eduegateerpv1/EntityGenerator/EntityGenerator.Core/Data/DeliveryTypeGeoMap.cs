using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryTypeGeoMaps", Schema = "distribution")]
    public partial class DeliveryTypeGeoMap
    {
        [Key]
        public long DeliveryTypeGeoMapIID { get; set; }
        public int? DeliveryTypeID { get; set; }
        public long? BranchID { get; set; }
        [StringLength(50)]
        public string Longitude { get; set; }
        [StringLength(50)]
        public string Latitude { get; set; }
        public long? AreaID { get; set; }

        [ForeignKey("BranchID")]
        [InverseProperty("DeliveryTypeGeoMaps")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("DeliveryTypeID")]
        [InverseProperty("DeliveryTypeGeoMaps")]
        public virtual DeliveryType1 DeliveryType { get; set; }
    }
}
