namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SchoolGeoMaps", Schema = "schools")]
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
