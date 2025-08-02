namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.SchoolGeoMaps")]
    public partial class SchoolGeoMap
    {
        [Key]
        public long SchoolGeoMapIID { get; set; }

        public int? SchoolID { get; set; }

        [StringLength(50)]
        public string Longitude { get; set; }

        [StringLength(50)]
        public string Latitude { get; set; }

        public long? AreaID { get; set; }
    }
}
