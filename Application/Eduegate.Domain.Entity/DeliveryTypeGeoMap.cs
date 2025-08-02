namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("distribution.DeliveryTypeGeoMaps")]
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

        public virtual Branch Branch { get; set; }

        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }
    }
}
